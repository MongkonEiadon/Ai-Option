using System;
using System.Net;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using iqoptionapi.exceptions;
using iqoptionapi.extensions;
using iqoptionapi.models;
using iqoptionapi.ws;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace iqoptionapi.http {
    public class IqOptionHttpClient : ObservableObject {
        private readonly ILogger _logger;

        public IqOptionHttpClient(string username, string password, string host = "iqoption.com") {
            Client = new RestClient(ApiEndPoint(host));
            LoginModel = new LoginModel {Email = username, Password = password};
            _logger = IqOptionLoggerFactory.CreateLogger();
        }

        public LoginModel LoginModel { get; }


        public string SecuredToken { get; private set; }
        public IRestClient Client { get; }

        protected static Uri ApiEndPoint(string host) {
            return new Uri($"https://{host}/api");
        }

        #region [Profile]

        private readonly Subject<Profile> _profileSubject = new Subject<Profile>();
        private Profile _profile;

        public Profile Profile {
            get => _profile;
            private set {
                _profileSubject.OnNext(value);
                _profile = value;
            }
        }

        public IObservable<Profile> ProfileObservable() {
            return _profileSubject;
        }

        #endregion


        #region Web-Methods

        public Task<string> GetTokenAsync() {
            var tcs = new TaskCompletionSource<string>();
            try {
                var client = new RestClient("https://auth.iqoption.com/api/v1.0/login");
                var request = new RestRequest(Method.POST) {RequestFormat = DataFormat.Json}
                    .AddHeader("Content-Type", "application/x-www-form-urlencoded")
                    .AddHeader("content-type", "multipart/form-data")
                    .AddHeader("Accept", "application/json")
                    .AddParameter("email", LoginModel.Email, ParameterType.QueryString)
                    .AddParameter("password", LoginModel.Password, ParameterType.QueryString);

                var result = client.ExecuteTaskAsync(request)
                    .ContinueWith(t => { });
            }
            catch (Exception ex) {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        public async Task<IRestResponse> LoginAsync() {
            var result = await Client.ExecuteTaskAsync(new LoginV2Request(LoginModel));

            switch (result.StatusCode) {
                //200
                case HttpStatusCode.OK: {
                    var loginResult = result.Content.JsonAs<IqHttpResult<Profile>>();
                    if (loginResult.IsSuccessful) {
                        Client.CookieContainer = new CookieContainer();
                        foreach (var c in result.Cookies) {
                            if (c.Name.ToLower() == "ssid") SecuredToken = c.Value;
                            Client.CookieContainer?.Add(new Cookie(c.Name, c.Value, c.Path, c.Domain));
                        }

                        Profile = loginResult.Result;
                    }
                    else {
                        _logger.LogError(result.Content);
                        var exception = result.Content.JsonAs<IqHttpResult<LoginTooMuchResultMessage>>();
                        throw new LoginLimitExceededException(exception.Result.Ttl);
                    }

                    break;
                }

                //204
                case HttpStatusCode.NoContent: {
                    break;
                }

                //208
                case HttpStatusCode.AlreadyReported: {
                    _logger.LogError(result.Content);
                    var exception = result.Content.JsonAs<LoginFailedResultMessage>();
                    throw new LoginFailedException(LoginModel.Email, exception.Message);
                }

                default: {
                    _logger.LogWarning(
                        $"Can't determine login-result with status code : {result.StatusCode}, content : {result.Content}");
                    throw new LoginFailedException(LoginModel.Email, result.Content);
                }
            }


            return result;
        }

        public Task<IRestResponse> GetProfileAsync() {
            return ExecuteHttpClientAsync(new GetProfileRequest());
        }

        public async Task<IqHttpResult<IHttpResultMessage>> ChangeBalanceAsync(long balanceId) {
            var result = await ExecuteHttpClientAsync(new ChangeBalanceRequest(balanceId));

            if (result.StatusCode == HttpStatusCode.OK)
                return result.Content.JsonAs<IqHttpResult<IHttpResultMessage>>();

            return null;
        }

        private Task<IRestResponse> ExecuteHttpClientAsync(IRestRequest request) {
            var result = Client.ExecuteTaskAsync(request);
            return result;
        }

        #endregion
    }
}