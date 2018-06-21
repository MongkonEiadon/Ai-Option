using System;
using System.Net;
using System.Net.Http;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using iqoptionapi.exceptions;
using iqoptionapi.extensions;
using iqoptionapi.models;
using iqoptionapi.ws;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace iqoptionapi.http
{
    public class IqOptionHttpClient : ObservableObject {

        public LoginModel LoginModel { get; }

        private ILogger _logger;
        protected static Uri ApiEndPoint(string host) => new Uri($"https://{host}/api");
        public string SecuredToken { get; private set; }
        public IRestClient Client { get; private set; }

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

        public IObservable<Profile> ProfileObservable() => _profileSubject;

        #endregion

        public IqOptionHttpClient(string username, string password, string host = "iqoption.com")
        {
            Client = new RestClient(ApiEndPoint(host));
            LoginModel = new LoginModel() {Email = username, Password = password};
            _logger = IqOptionLoggerFactory.CreateLogger();
        }

        #region Web-Methods

        public async Task<IRestResponse> LoginAsync() {

            var result = await Client.ExecuteTaskAsync(new LoginV2Request(LoginModel));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var loginResult = result.Content.JsonAs<IqHttpResult<Profile>>();
                if (loginResult.IsSuccessful)
                {
                    this.Client.CookieContainer = new CookieContainer();
                    foreach (var c in result.Cookies)
                    {
                        if (c.Name.ToLower() == "ssid")
                        {
                            SecuredToken = c.Value;
                        }
                        this.Client.CookieContainer?.Add(new Cookie(c.Name, c.Value, c.Path, c.Domain));
                    }
                    this.Profile = loginResult.Result;

                }

                else {
                    _logger.LogError(result.Content);
                    var exception = result.Content.JsonAs<IqHttpResult<LoginFailedResultMessage>>();
                    throw new LoginLimitExceededException(exception.Result.Ttl);
                }
            }

            else {

                _logger.LogCritical($"Failed Login : {LoginModel.Email}");
                _logger.LogCritical($"content = {result}");

                throw new LoginFailedException(LoginModel.Email, result);
            }


            return result;
        }

        public Task<IRestResponse> GetProfileAsync(){
            return ExecuteHttpClientAsync(new GetProfileRequest());
        }

        public async Task<IqHttpResult<IHttpResultMessage>> ChangeBalanceAsync(long balanceId) {
            var result = await ExecuteHttpClientAsync(new ChangeBalanceRequest(balanceId));

            if (result.StatusCode == HttpStatusCode.OK) {
                return result.Content.JsonAs<IqHttpResult<IHttpResultMessage>>();
            }
            
            return null;
        }

        private Task<IRestResponse> ExecuteHttpClientAsync(IRestRequest request) {
            _logger.LogTrace(request.AsJson());

            var result = Client.ExecuteTaskAsync(request)
                .ContinueWith(t => {
                    _logger.LogTrace(t.Result.AsJson());
                    return t;
                }).Unwrap();

            return result;
        }


        #endregion



    }
}

    
