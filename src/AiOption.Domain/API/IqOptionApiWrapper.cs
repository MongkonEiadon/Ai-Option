using System;
using System.Threading.Tasks;

namespace AiOption.Domain.API {

    public interface IIqOptionApiWrapper {

        Task<Tuple<bool, string>> LoginToIqOptionAsync(string email, string password);

    }

}