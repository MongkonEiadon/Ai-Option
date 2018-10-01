using System;
using System.Threading.Tasks;

namespace AiOption.Application.API {

    public interface IIqOptionApiWrapper {

        Task<Tuple<bool, string>> LoginToIqOptionAsync(string email, string password);

    }

}