using System;
using System.Threading.Tasks;

using AiOption.Domain.API;

using iqoptionapi.http;

namespace AiOption.Infrastructure.Api {

    public class IqOptionLoginWrapper : IIqOptionApiWrapper {

        public async Task<Tuple<bool, string>> LoginToIqOptionAsync(string email, string password) {
            var api = new IqOptionHttpClient(email, password);

            var result = await api.LoginAsync();
            if (result.IsSuccessful) return new Tuple<bool, string>(true, result.Data.Ssid);

            return new Tuple<bool, string>(false, result.Errors.GetErrorMessage());
        }

    }

}