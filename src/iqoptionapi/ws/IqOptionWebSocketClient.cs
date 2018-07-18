using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using iqoptionapi.extensions;
using iqoptionapi.models;
using iqoptionapi.ws.request;
using Microsoft.Extensions.Logging;
using WebSocket4Net;

namespace iqoptionapi.ws {
    public class IqOptionWebSocketClient : IDisposable {
        //privates
        private readonly ILogger _logger = IqOptionLoggerFactory.CreateLogger();
        private readonly Subject<long> _timeSyncSubject = new Subject<long>();


        private long _timeSync;

        public IqOptionWebSocketClient(string secureToken, Action<IqOptionWebSocketClient> initialSetup, string host = "iqoption.com") {

            Client = new WebSocket($"wss://{host}/echo/websocket");
            SecureToken = secureToken;

            //set up shred obs.
            InstrumentResultSetObservable =
                _instrumentResultSetSubject.PublishLast().RefCount();

            BuyResultObservable =
                _buyResulSjSubject.Publish().RefCount();

            MessageReceivedObservable =
                Observable.Using(
                        () => Client,
                        _ => Observable
                            .FromEventPattern<EventHandler<MessageReceivedEventArgs>, MessageReceivedEventArgs>(
                                handler => _.MessageReceived += handler,
                                handler => _.MessageReceived -= handler))
                    .Select(x => x.EventArgs.Message)
                    .SubscribeOn(new EventLoopScheduler())
                    .Publish()
                    .RefCount();


            MessageReceivedObservable.Subscribe(x => {
                var a = x.JsonAs<WsRequestMessageBase<object>>();
                switch (a.Name?.ToLower())
                {
                    case "heartbeat":
                        {
                            break;
                        }

                    case "timesync":
                        {
                            _timeSync = long.Parse(a.Message.ToString());
                            _timeSyncSubject.OnNext(_timeSync);
                            break;
                        }
                    case "profile":
                        {
                            Profile = x.JsonAs<WsRequestMessageBase<Profile>>().Message;
                            _logger.LogTrace($"Received Prof. => {Profile?.Email}");

                            break;
                        }
                    case "instruments":
                        {
                            var result = x.JsonAs<WsRequestMessageBase<InstrumentsResult>>().Message;
                            _logger.LogTrace($"Received Inst. => instruments ({result.Type.ToString()})");
                            _instrumentResultSet[result.Type] = result.Instruments;
                            _instrumentResultSetSubject.OnNext(_instrumentResultSet);

                            if (_instrumentResultSet.All(i => i.Value.Any())) _instrumentResultSetSubject.OnCompleted();

                            break;
                        }

                    case "profit-top-user-moved-up":
                        {
                            break;
                        }

                    case "activecommissionchange":
                        {
                            break;
                        }

                    case "user-tournament-position-changed":
                        {
                            break;
                        }

                    case "chat-state-updated":
                        {
                            break;
                        }
                    case "front":
                        {
                            break;
                        }

                    case "listinfodata":
                        {
                            var result = x.JsonAs<WsRequestMessageBase<InfoData[]>>();
                            _infoDataSubject.OnNext(result?.Message);
                            var info = result?.Message.FirstOrDefault();
                            if (info != null)
                                _logger.LogInformation(
                                    $"info-received  => {info.UserId} {info.Win} {info.Direction} {info.Sum} {info.Active} @{info.Value}");
                            break;
                        }

                    case "buycomplete":
                        {
                            var result = x.JsonAs<WsRequestMessageBase<WsMsgResult<object>>>().Message;
                            if (result.IsSuccessful)
                            {
                                var buyResult = x.JsonAs<WsRequestMessageBase<WsMsgResult<BuyResult>>>().Message.Result;
                                _logger.LogInformation(
                                    $"buycompleted   => {buyResult.UserId} {buyResult.Type} {buyResult.Direction} {buyResult.Price} {(ActivePair)buyResult.Act} @{buyResult.Value} ");
                                _buyResulSjSubject.OnNext(buyResult);
                            }
                            else
                            {
                                var ex = string.Join(", ", result.Message?.ToList());
                                _logger.LogError($"{this.Profile?.UserId}\t{ex}", ex);
                                //_buyResulSjSubject.OnNext(BuyResult.BuyResultError(result.Message));
                            }

                            break;
                        }

                    default:
                        {
                            _logger.LogDebug(Profile?.Id + "    =>  " + a.AsJson());
                            break;
                        }
                }
            }, ex => { _logger.LogCritical(ex.Message); });


            //send ssid message
            OpenSecuredSocketAsync();


            initialSetup?.Invoke(this);
        }

        public IqOptionWebSocketClient(string secureToken, string host = "iqoption.com") : this(secureToken, null, host) { }

        private WebSocket Client { get; }
        public DateTime TimeSync => _timeSync.FromUnixToDateTime();


        public void Dispose() {
            Client?.Dispose();
        }


        public async Task SendMessageAsync(IWsIqOptionMessageCreator messageCreator) {
            if (await OpenWebSocketAsync()) {
                _logger.LogTrace($"send message   => :\t{messageCreator.CreateIqOptionMessage()}");
                Client.Send(messageCreator.CreateIqOptionMessage());
            }
        }

        public Task<InstrumentResultSet> SendInstrumentsRequestAsync() {
            var tcs = new TaskCompletionSource<InstrumentResultSet>();
            try {
                _logger.LogDebug(nameof(SendInstrumentsRequestAsync));

                //subscribe for the lastest result
                InstrumentResultSetObservable
                    .Subscribe(x => { tcs.TrySetResult(x); });

                //clear before query new 
                _instrumentResultSet = new InstrumentResultSet();

                //execute them all
                Task.WhenAll(
                    SendMessageAsync(GetInstrumentWsRequestMessageBase.CreateRequest(InstrumentType.Forex)),
                    SendMessageAsync(GetInstrumentWsRequestMessageBase.CreateRequest(InstrumentType.CFD)),
                    SendMessageAsync(GetInstrumentWsRequestMessageBase.CreateRequest(InstrumentType.Crypto))
                );
            }

            catch (Exception ex) {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        public Task<BuyResult> BuyAsync(
            ActivePair pair,
            int size,
            OrderDirection direction,
            DateTime expiration = default(DateTime)) {
            var tcs = new TaskCompletionSource<BuyResult>();
            try {
                var obs = BuyResultObservable
                    .OnErrorResumeNext(BuyResultObservable)
                    .Where(x => x != null)
                    .Subscribe(x => { tcs.TrySetResult(x); },
                        ex => { tcs.TrySetException(ex); });

                tcs.Task.ContinueWith(t => {
                    if (t.Result != null) {
                        obs.Dispose();
                    }
                });

                SendMessageAsync(new BuyV2WsRequestMessage(pair, size, direction, expiration,
                    TimeSync.ToLocalTime())).ConfigureAwait(false);
            }
            catch (Exception ex) {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }


        public async Task<bool> OpenWebSocketAsync() {
            if (Client.State == WebSocketState.Open)
                return true;

            return await Client.OpenAsync();
        }

        private Task OpenSecuredSocketAsync() {
            return SendMessageAsync(new SsidWsRequestMessageBase(SecureToken));
        }

        #region [Public's]

        public IObservable<string> MessageReceivedObservable { get; }
        
        public string SecureToken { get; set; }

        #endregion

        #region [Profile]

        private Profile _profile;
        private readonly Subject<Profile> _profileSubject = new Subject<Profile>();

        public Profile Profile {
            get => _profile;
            set {
                _profile = value;
                _profileSubject.OnNext(value);
            }
        }

        public IObservable<Profile> ProfileObservable => _profileSubject;

        #endregion

        #region [Instruments]

        private InstrumentResultSet _instrumentResultSet = new InstrumentResultSet();
        private readonly Subject<InstrumentResultSet> _instrumentResultSetSubject = new Subject<InstrumentResultSet>();

        public IObservable<InstrumentResultSet> InstrumentResultSetObservable { get; }

        #endregion

        #region [InfoData]

        private readonly Subject<InfoData[]> _infoDataSubject = new Subject<InfoData[]>();
        public IObservable<InfoData[]> InfoDataObservable => _infoDataSubject.Publish().RefCount();

        #endregion

        #region [BuyV2]

        private readonly Subject<BuyResult> _buyResulSjSubject = new Subject<BuyResult>();
        public IObservable<BuyResult> BuyResultObservable { get; }

        #endregion
    }
}