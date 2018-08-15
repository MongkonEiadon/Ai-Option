using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using AiOption.Domain.IqOptionAccount;

namespace AiOption.Application.Repositories.ReadOnly
{

    public interface IIqOptionAccountReadOnlyRepository {

        Task<IEnumerable<IqOptionAccount>> GetAllTask();
        Task<IqOptionAccount> GetByUserIdTask(int userId);
        Task<IqOptionAccount> GetByUserNameTask(string userName);

    }


}
