using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.IqAccounts;
using AiOption.Infrasturcture.ReadStores.ReadModels;
using AiOption.Query.IqAccounts;
using EventFlow.Queries;
using EventFlow.ReadStores;

namespace AiOption.Infrasturcture.ReadStores.QueryHandlers.IqAccounts
{
    internal class GetIqAccountByIdQueryHandler : IQueryHandler<QueryIqAccountById, IqAccount>
    {
        private readonly IReadModelStore<IqAccountReadModelDto> _readModelStore;

        public GetIqAccountByIdQueryHandler(IReadModelStore<IqAccountReadModelDto> readModelStore)
        {
            _readModelStore = readModelStore;
        }

        public async Task<IqAccount> ExecuteQueryAsync(QueryIqAccountById queryIqAccount,
            CancellationToken cancellationToken)
        {
            var result = await _readModelStore.GetAsync(queryIqAccount.AccountId.Value, cancellationToken);

            return result.ReadModel?.ToIqAccount();
        }
    }
}