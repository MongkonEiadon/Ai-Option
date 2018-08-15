using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AiOption.Application.Repositories.WriteOnly
{

    public interface IIqOptionWriteOnlyRepository {
        Task<bool> UpdateSecuredToken(int userId, string securedToken);

    }

}
