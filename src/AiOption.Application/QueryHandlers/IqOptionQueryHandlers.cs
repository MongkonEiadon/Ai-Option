using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Application.Repositories.ReadOnly;
using AiOption.Domain.Customers;
using AiOption.Domain.IqAccounts;
using AiOption.Domain.IqAccounts.Queries;

using EventFlow.Queries;

namespace AiOption.Application.QueryHandlers {

    public class IqOptionQueryHandlers :
        IQueryHandler<GetFollowerAccountToOpenTradingsQuery, IEnumerable<Account>>,
        IQueryHandler<GetTraderAccountToOpenTradingsQuery, IEnumerable<Account>>,
        IQueryHandler<GetAccountByAccoutIdQuery, Account> {

        private readonly IIqOptionAccountReadOnlyRepository _readonlyAccountRepo;

        public IqOptionQueryHandlers(
            IIqOptionAccountReadOnlyRepository _readonlyAccountRepo
        ) {
            this._readonlyAccountRepo = _readonlyAccountRepo;
        }

        public Task<Account> ExecuteQueryAsync(GetAccountByAccoutIdQuery query, CancellationToken cancellationToken) {
            return _readonlyAccountRepo.GetByUserIdTask(query.UserId);
        }


        public async Task<IEnumerable<Account>> ExecuteQueryAsync(GetFollowerAccountToOpenTradingsQuery query,
            CancellationToken cancellationToken) {

            var invalidFollowerLevels = new[] {CustomerLevel.Administrator, CustomerLevel.Traders, CustomerLevel.Baned};

            return (await _readonlyAccountRepo.GetActiveAccountForOpenTradingsAsync())
                .Where(x => !invalidFollowerLevels.Contains(x.Level));
        }

        public async Task<IEnumerable<Account>> ExecuteQueryAsync(GetTraderAccountToOpenTradingsQuery query,
            CancellationToken cancellationToken) {
            return (await _readonlyAccountRepo.GetActiveAccountForOpenTradingsAsync())
                .Where(x => x.Level == CustomerLevel.Traders);


        }

    }

}