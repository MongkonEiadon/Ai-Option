using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.IqAccounts;
using EventFlow.Queries;

namespace AiOption.Query.IqAccounts
{
    public abstract class QueryIqAccountById : IQuery<IqAccount>
    {
        public QueryIqAccountById(IqAccountId accountId)
        {
            AccountId = accountId;
        }

        public IqAccountId AccountId { get; }
    }


    internal class QueryIqAccountByIdQueryHandler : IQueryHandler<QueryIqAccountById, IqAccount>
    {
        private readonly ISearchableReadModelStore<IqAccountReadModel> _readStore;

        public QueryIqAccountByIdQueryHandler(
            ISearchableReadModelStore<IqAccountReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<IqAccount> ExecuteQueryAsync(QueryIqAccountById queryIqAccount,
            CancellationToken cancellationToken)
        {
            var result = await _readStore.GetAsync(queryIqAccount.AccountId.Value, cancellationToken);

            return result.ReadModel?.ToIqAccount();
        }
    }
}