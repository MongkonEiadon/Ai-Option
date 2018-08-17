using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Application.Repositories.ReadOnly;
using AiOption.Domain.IqOption;
using AiOption.Domain.IqOption.Queries;
using EventFlow.Queries;

namespace AiOption.Application.QueryHandlers
{
    public class IqOptionQueryHandlers : 
        IQueryHandler<GetCustomerAccountsForTradingQuery, IEnumerable<Account>>
    {
        private readonly IIqOptionAccountReadOnlyRepository _readonlyAccountRepo;

        public IqOptionQueryHandlers(
            IIqOptionAccountReadOnlyRepository _readonlyAccountRepo
            )
        {
            this._readonlyAccountRepo = _readonlyAccountRepo;
        }


        public Task<IEnumerable<Account>> ExecuteQueryAsync(GetCustomerAccountsForTradingQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
