using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using iqoption.core;
using iqoption.core.Extensions;
using iqoption.domain.IqOption.Command;
using Newtonsoft.Json;
using RestSharp;

namespace iqoption.apiservice {
    public class LoginCommandHandler : ValidatorHandler<LoginCommand, LoginCommandResult>, ILoginCommandHandler {
        public override Task<LoginCommandResult> HandleRequestAsync(LoginCommand command,
            CancellationToken ct = default(CancellationToken)) {
            var tcs = new TaskCompletionSource<LoginCommandResult>();
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
                                tcs.TrySetResult(new LoginCommandResult(result.Result.Ssid, true, null));
                                break;
                            }

                            case HttpStatusCode.BadRequest: {
                                var error = t.Result.Content.JsonAs<LoginErrorCommandResult>();
                                tcs.TrySetResult(new LoginCommandResult(null, false, string.Join(",",
                                    error.Errors?.Select(x => x.Title)?.ToList())));

                                break;
                            }

                            case HttpStatusCode.Forbidden: {
                                var error = t.Result.Content.JsonAs<LoginErrorCommandResult>();
                                tcs.TrySetResult(new LoginCommandResult(null, false, string.Join(",",
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
    }

    internal class LoginResultModel : HttpCommandResult<SsidResult> {
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