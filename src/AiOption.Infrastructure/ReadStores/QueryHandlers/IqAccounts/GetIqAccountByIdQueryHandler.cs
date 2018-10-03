using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.IqAccounts;
using AiOption.Infrastructure.ReadStores.ReadModels;
using AiOption.Query.IqAccounts;
using EventFlow.Queries;
using EventFlow.ReadStores;

namespace AiOption.Infrastructure.ReadStores.QueryHandlers.IqAccounts
{
    class GetIqAccountByIdQueryHandler : IQueryHandler<QueryIqAccountById, IqAccount>
    {
        private readonly IReadModelStore<IqAccountReadModel> _readModelStore;

        public GetIqAccountByIdQueryHandler(IReadModelStore<IqAccountReadModel> readModelStore)
        {
            _readModelStore = readModelStore;
        }

        public async Task<IqAccount> ExecuteQueryAsync(QueryIqAccountById queryIqAccount, CancellationToken cancellationToken)
        {
            var result = await _readModelStore.GetAsync(queryIqAccount.AccountId.Value, cancellationToken);

            return result.ReadModel?.ToIqAccount();
        }
    }
}