using System.Collections.Generic;
using System.Threading.Tasks;

using AiOption.Domain.IqAccounts;

namespace AiOption.Application.Repositories.ReadOnly {

    public interface IIqOptionAccountReadOnlyRepository {

        Task<Account> GetByUserIdTask(int userId);
        Task<IEnumerable<Account>> GetActiveAccountForOpenTradingsAsync();

    }


}