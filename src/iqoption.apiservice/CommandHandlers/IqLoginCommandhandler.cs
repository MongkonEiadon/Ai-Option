using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Commands;
using iqoption.core;
using iqoption.core.data;
using iqoption.core.Extensions;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Command;
using iqoption.domain.IqOption.Commands;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace iqoption.apiservice.CommandHandlers {

    public class IqLoginCommandhandler :
        ICommandHandler<IqOptionAggregate, IqOptionIdentity, ValidateSecuredTokenCommandResult, ValidateSecureTokenCommand>,
        ICommandHandler<IqOptionAggregate, IqOptionIdentity, IqLoginCommandResult, IqLoginCommand> {


        private readonly ILogger _logger;
        private readonly ICommandBus _commandBus;

        public IqLoginCommandhandler(ILogger logger, ICommandBus commandBus) {
            _logger = logger;
            _commandBus = commandBus;
        }

        public Task<IqLoginCommandResult> ExecuteCommandAsync(IqOptionAggregate aggregate, IqLoginCommand command, CancellationToken ct) {
            
            var tcs = new TaskCompletionSource<IqLoginCommandResult>();
            try {
                var client = new RestClient("https://auth.iqoption.com/api/v1.0/login");
                var request = new RestRequest(Method.POST) {RequestFormat = DataFormat.Json}
                    .AddHeader("Content-Type", "application/x-www-form-urlencoded")
                    .AddHeader("content-type", "multipart/form-data")
                    .AddHeader("Accept", "application/json")
                    .AddParameter("email", command.Email, ParameterType.QueryString)
                    .AddParameter("password", command.Password, ParameterType.QueryString);

                client.ExecuteTaskAsync(request, ct)
                    .ContinueWith(t => {
                        switch (t.Result.StatusCode) {
                            case HttpStatusCode.OK: {
                                var result = t.Result.Content.JsonAs<LoginResultModel>();
                                tcs.TrySetResult(new IqLoginCommandResult(result.Result.Ssid, true, null));
                                break;
                            }

                            case HttpStatusCode.BadRequest: {
                                var error = t.Result.Content.JsonAs<LoginErrorCommandResult>();
                                tcs.TrySetResult(new IqLoginCommandResult(null, false, string.Join(",",
                                    error.Errors?.Select(x => x.Title)?.ToList())));

                                break;
                            }

                            case HttpStatusCode.Forbidden: {
                                var error = t.Result.Content.JsonAs<LoginErrorCommandResult>();
                                tcs.TrySetResult(new IqLoginCommandResult(null, false, string.Join(",",
                                    error.Errors?.Select(x => x.Title)?.ToList())));

                                break;
                            }

                            case HttpStatusCode.Moved: {
                                var error = t.Result.Content.JsonAs<LoginErrorCommandResult>();
                                tcs.TrySetResult(new IqLoginCommandResult(null, false, string.Join(",",
                                    error.Errors?.Select(x => x.Title)?.ToList())));

                                break;
                            }
                        }

                        tcs.TrySetException(new Exception($"Error when get token with {t.Result.Content}"));
                    }, ct);
            }
            catch (Exception ex) {
                tcs.TrySetException(ex);
            }

           

            return tcs.Task;
        }

        public Task<ValidateSecuredTokenCommandResult> ExecuteCommandAsync(
            IqOptionAggregate aggregate, 
            ValidateSecureTokenCommand command,
            CancellationToken cancellationToken) {

            var tcs = new TaskCompletionSource<ValidateSecuredTokenCommandResult>();
            try {

                var client = new RestClient("https://iqoption.com/api/getprofile");
                var request = new RestRequest(Method.GET)
                    .AddCookie("ssid", command.SecuredToken);

                var result = client.ExecuteTaskAsync(request, cancellationToken)
                    .ContinueWith(t => {
                        if (t.Result.IsSuccessful && t.Result.StatusCode == HttpStatusCode.OK) {

                            var httpResult = t.Result.Content.JsonAs<IqHttpResult<dynamic>>();

                            if (httpResult.IsSuccessful) {

                                tcs.TrySetResult(new ValidateSecuredTokenCommandResult(true,
                                    t.Result.Content.JsonAs<IqHttpResult<Profile>>().Result));
                            }
                        }

                        tcs.TrySetResult(new ValidateSecuredTokenCommandResult(false, null));
                    }, cancellationToken);

            }
            catch (Exception ex) {
                tcs.TrySetException(ex);
            }

            return tcs.Task;

        }
    }


    internal class LoginResultModel : HttpCommandResult<SsidResult> {
    }

    public class IqHttpResult<T>
    {
        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("message")]
        public object Message { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }

    internal class HttpCommandResult<T> {
        [JsonProperty("data")]
        public T Result { get; set; }
    }

    internal class SsidResult {
        [JsonProperty("ssid")]
        public string Ssid { get; set; }
    }
}