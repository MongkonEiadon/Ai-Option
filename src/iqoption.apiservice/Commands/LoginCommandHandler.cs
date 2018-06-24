using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using iqoption.core.Extensions;
using iqoption.domain.IqOption.Command;
using iqoption.domain.IqOption.Exceptions;
using RestSharp;

namespace iqoption.apiservice
{
    public class LoginCommandHandler : ILoginCommandHandler {

        public LoginCommandHandler() {

        }

        public Task<string> ExecuteAsync(ILoginCommand command) {

            var tcs = new TaskCompletionSource<string>();
            try {
                var client = new RestClient("https://auth.iqoption.com/api/v1.0/login");
                var request = new RestRequest(Method.POST) {RequestFormat = DataFormat.Json}
                    .AddHeader("Content-Type", "application/x-www-form-urlencoded")
                    .AddHeader("content-type", "multipart/form-data")
                    .AddHeader("Accept", "application/json")
                    .AddParameter("email", command.Email, ParameterType.QueryString)
                    .AddParameter("password", command.Password, ParameterType.QueryString);


                client.ExecuteTaskAsync(request)
                    .ContinueWith(t => {
                        switch (t.Result.StatusCode) {
                            case HttpStatusCode.OK: {
                                var result = t.Result.Content.JsonAs<LoginCommandResult>();
                                tcs.TrySetResult(result.Result.Ssid);
                                break;
                            }

                            case HttpStatusCode.BadRequest:
                            {
                                var error = t.Result.Content.JsonAs<LoginErrorCommandResult>();
                                tcs.TrySetException(new LoginFailedException(String.Join(",", error.Errors?.Select(x => x.Title)?.ToList())));

                                break;
                            }

                            case HttpStatusCode.Forbidden: {
                                var error = t.Result.Content.JsonAs<LoginErrorCommandResult>();
                                tcs.TrySetException(new LoginFailedException(String.Join(",", error.Errors?.Select(x => x.Title)?.ToList())));

                                break;
                            }
                        }

                        tcs.TrySetException(new Exception($"Error when get token with {t.Result.Content}"));
                    });



            }
            catch (Exception ex) {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }
    }
}
