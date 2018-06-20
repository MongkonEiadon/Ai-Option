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
    public interface IIqOptionApi : IDisposable {
        IqOptionWebSocketClient WsClient { get; }
        IqOptionHttpClient HttpClient { get; }

        IObservable<Profile> ProfileObservable { get; }
        IObservable<InfoData[]> InfoDatasObservable { get; }

        Profile Profile { get; }

        bool IsConnected { get; }

        IObservable<bool> IsConnectedObservable { get; }

        Task<string> GetTokenAsync();
        Task<bool> ConnectAsync();
        Task<Profile> GetProfileAsync();
        Task<bool> ChangeBalanceAsync(long balanceId);

        Task<BuyResult> BuyAsync(ActivePair pair, int size, OrderDirection direction,
            DateTime expiration = default(DateTime));
    }


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

        public Task<string> GetTokenAsync() {
            return HttpClient.LoginAsync().ContinueWith(async t => {
                await t;
                return HttpClient.SecuredToken;
            }).Unwrap();
        }

        public Task<bool> ConnectAsync() {
            connectedSubject.OnNext(false);
            IsConnected = false;

            var tcs = new TaskCompletionSource<bool>();
            try {
                HttpClient.LoginAsync()
                    .ContinueWith(async t => {
                        if ((await t).StatusCode == HttpStatusCode.OK) {
                            _logger.LogInformation(
                                $"{_configuration.Email} logged in to {_configuration.Host} success!");
                            WsClient = new IqOptionWebSocketClient(HttpClient.SecuredToken, _configuration.Host);

                            if (await WsClient.OpenWebSocketAsync()) SubscribeWebSocket();

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

        public async Task<Profile> GetProfileAsync() {
            var result = await HttpClient.GetProfileAsync();

            if (result.StatusCode == HttpStatusCode.OK && 
                result.Content.JsonAs<IqHttpResult<HttpResultMessage>>().IsSuccessful) {

                var profile = result.Content.JsonAs<IqHttpResult<Profile>>()?.Result;
                _logger.LogTrace($"Get Profile!: \t{profile.Email}");


                return profile;
            }

            _logger.LogError(result.Content);
            return null;
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
}