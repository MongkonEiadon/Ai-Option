using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventFlow.Queries;
using iqoption.core;
using iqoption.core.Extensions;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Command;
using RestSharp;
using Profile = iqoption.domain.IqOption.Profile;

namespace iqoption.apiservice.Queries {

    public class GetProfileQueryHandler : IQueryHandler<GetProfileQuery, GetProfileResult> {

        private readonly IMapper _mapper;

        public GetProfileQueryHandler(IMapper mapper) {
            _mapper = mapper;
        }

        public Task<GetProfileResult> ExecuteQueryAsync(GetProfileQuery query, CancellationToken ct)
        {
           
            var tcs = new TaskCompletionSource<GetProfileResult>();
            try
            {
                var client = new RestClient("https://iqoption.com/api/getprofile");
                var request = new RestRequest(Method.GET);
                request
                    .AddCookie("ssid", query.Ssid)
                    .AddHeader("Accept", "application/json");


                client.ExecuteTaskAsync(request, ct)
                    .ContinueWith(t => {
                        if (t.Result.StatusCode == HttpStatusCode.OK)
                        {
                            var result = t.Result.Content.JsonAs<IqHttpResult<Profile>>();

                            if (result.IsSuccessful) {
                                var profile = _mapper.Map<Profile>(result.Result);

                                tcs.TrySetResult(new GetProfileResult(profile));
                            }
                        }
                    }, ct);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

            return tcs.Task;

        }
    }
}