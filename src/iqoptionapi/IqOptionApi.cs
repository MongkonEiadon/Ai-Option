using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using iqoptionapi.extensions;
using iqoptionapi.http;
using iqoptionapi.models;
using iqoptionapi.ws;
using Microsoft.Extensions.Logging;

namespace iqoptionapi {
    public class IqOptionApi : IIqOptionApi {
        private readonly IqOptionConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly Subject<Profile> _profileSubject = new Subject<Profile>();
        private Profile _profile;

        private readonly Subject<bool> connectedSubject = new Subject<bool>();

        public IDictionary<InstrumentType, Instrument[]> Instruments { get; private set; }

        public IObservable<InfoData[]> InfoDatasObservable { get; private set; }
        public IqOptionHttpClient HttpClient { get; }
        public IqOptionWebSocketClient WsClient { get; private set; }

        public Profile Profile {
            get => _profile;
            private set {
                _profileSubject.OnNext(value);
                _profile = value;
            }
        }

        public IObservable<Profile> ProfileObservable => _profileSubject;
        public bool IsConnected { get; private set; }

        public IObservable<bool> IsConnectedObservable => connectedSubject;


        public Task<Profile> LoginAsync() {

            var tcs = new TaskCompletionSource<Profile>();

            try {
                var t = HttpClient.LoginAsync();
                

                tcs.TrySetResult(HttpClient.Profile);
            }
            catch (AggregateException aggregateException) {
                tcs.TrySetException(aggregateException.Flatten());
            }
            catch (Exception ex) {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        public Task<bool> ConnectAsync() {
            connectedSubject.OnNext(false);
            IsConnected = false;

            var tcs = new TaskCompletionSource<bool>();
            try {
                HttpClient
                    .LoginAsync()
                    .ContinueWith(async t => {
                        if ((await t).StatusCode == HttpStatusCode.OK) {
                            _logger.LogInformation(
                                $"{_configuration.Email} logged in to {_configuration.Host} success!");
                            WsClient = new IqOptionWebSocketClient(HttpClient.SecuredToken, _configuration.Host);

                            if (await WsClient.OpenWebSocketAsync())
                                SubscribeWebSocket();

                            var profile = await GetProfileAsync();

                            _logger.LogInformation($"WebSocket for {profile.Email}({profile.UserId}) Connected!");

                            IsConnected = true;
                            connectedSubject.OnNext(true);
                            tcs.TrySetResult(true);
                        }

                        tcs.TrySetResult(false);
                    });
            }
            catch (Exception ex) {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        public Task<Profile> GetProfileAsync() {

            var tcs = new TaskCompletionSource<Profile>();

            try {

                HttpClient
                    .GetProfileAsync()
                    .ContinueWith(async t => {

                        if ((await t).StatusCode == HttpStatusCode.OK) {

                            if ((await t).Content.TryParseJson(out IqHttpResult<Profile> content)) {
                                tcs.TrySetResult(content.Result);
                            }
                        }

                        tcs.TrySetException(new IqOptionApiGetProfileFailedException($"token = '' & content = '{(await t).Content}'"));

                        return tcs.Task;
                    });

            }
            catch (Exception ex) {
                _logger.LogCritical(ex, nameof(GetProfileAsync));
                tcs.TrySetException(ex);
            }
            
            return tcs.Task;
        }

        public async Task<bool> ChangeBalanceAsync(long balanceId) {
            var result = await HttpClient.ChangeBalanceAsync(balanceId);

            if (result?.Message == null && !result.IsSuccessful) {
                _logger.LogError($"Change balance ({balanceId}) error : {result.Message}");
                return false;
            }

            return true;
        }

        public async Task<BuyResult> BuyAsync(ActivePair pair, int size, OrderDirection direction,
            DateTime expiration = default(DateTime)) {
            return await WsClient.BuyAsync(pair, size, direction, expiration);
        }


        public void Dispose() {
            connectedSubject?.Dispose();
            WsClient?.Dispose();
        }


        public Task<InstrumentResultSet> GetInstrumentsAsync() {
            return WsClient.SendInstrumentsRequestAsync();
        }


        private void SubscribeWebSocket() {
            Contract.Requires(WsClient != null);
            Contract.Requires(HttpClient != null);

            //subscribe profile
            WsClient.ProfileObservable
                .Merge(HttpClient.ProfileObservable())
                .DistinctUntilChanged()
                .Subscribe(x => {
                    _logger.LogTrace($"Profile Updated : {x?.ToString()}");
                    Profile = x;
                });

            WsClient.InstrumentResultSetObservable
                .Subscribe(x => {
                    _logger.LogTrace($"Instrument Updated!");
                    Instruments = x;
                });

            InfoDatasObservable = WsClient.InfoDataObservable;
        }

        #region [Ctor]

        public IqOptionApi(string username, string password, string host = "iqoption.com")
            : this(new IqOptionConfiguration {Email = username, Password = password, Host = host}) {
        }

        public IqOptionApi(IqOptionConfiguration configuration) {
            _configuration = configuration;

            //set up client
            HttpClient = new IqOptionHttpClient(configuration.Email, configuration.Password);

            //set log
            _logger = IqOptionLoggerFactory.CreateLogger();
        }

        #endregion
    }

    public enum OrderDirection {
        [EnumMember(Value = "put")] Put = 1,

        [EnumMember(Value = "call")] Call = 2
    }


    public class IqOptionApiGetProfileFailedException : Exception {
        public IqOptionApiGetProfileFailedException(object receivedContent) : base(
            $"received incorrect content : {receivedContent}") {
        }
    }
}