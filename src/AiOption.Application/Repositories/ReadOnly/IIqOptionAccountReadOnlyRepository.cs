using System.Collections.Generic;
using System.Threading.Tasks;

using AiOption.Domain.Accounts;

namespace AiOption.Application.Repositories.ReadOnly {

    public interface IIqOptionAccountReadOnlyRepository {

        Task<IEnumerable<Account>> GetAllTask();
        Task<Account> GetByUserIdTask(int userId);
        Task<Account> GetByUserNameTask(string userName);

        Task<IEnumerable<Account>> GetActiveAccountForOpenTradingsAsync();

    }


}