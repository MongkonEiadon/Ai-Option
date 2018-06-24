using System;
using System.Net;
using System.Threading.Tasks;
using iqoption.core.Extensions;
using iqoption.domain.IqOption;
using RestSharp;

namespace iqoption.apiservice.Queries {

    public interface IGetProfileCommandHandler {
        Task<Profile> RetreiveProfileQueryAsync(string ssid);
    }
    public class GetProfileCommandHandler : IGetProfileCommandHandler {

        public Task<Profile> RetreiveProfileQueryAsync(string ssid) {

            var tcs = new TaskCompletionSource<Profile>();
            try {

                var client = new RestClient("https://iqoption.com/api/getprofile");
                var request = new RestRequest(Method.GET);
                request
                    .AddCookie("ssid", ssid)
                    .AddHeader("Accept", "application/json");


                client.ExecuteTaskAsync(request)
                    .ContinueWith(t => {

                        if (t.Result.StatusCode == HttpStatusCode.OK) {
                            var result =  t.Result.Content.JsonAs<IqHttpResult<Profile>>();

                            tcs.TrySetResult(result.Result);

                        }


                    });

            }
            catch (Exception ex) {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }
    }
}