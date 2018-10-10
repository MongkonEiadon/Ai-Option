using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Domain.IqAccounts;
using AiOption.Infrasturcture.ReadStores.ReadModels;
using AiOption.Query;
using AiOption.Query.IqAccounts;
using EventFlow.EntityFramework;
using EventFlow.Queries;
using EventFlow.ReadStores;
using Microsoft.EntityFrameworkCore;

namespace AiOption.Infrasturcture.ReadStores.QueryHandlers.IqAccounts
{
    internal class GetIqAccountByIdQueryHandler : 
        IQueryHandler<QueryIqAccountById, IqAccount>,
        IQueryHandler<QueryIqAccountByEmailAddress, IqAccount>
    {
        private readonly ISearchableReadModelStore<IqAccountReadModelDto> _readStore;

        public GetIqAccountByIdQueryHandler(
            ISearchableReadModelStore<IqAccountReadModelDto> readStore)
        {
            _readStore = readStore;
        }

        public async Task<IqAccount> ExecuteQueryAsync(QueryIqAccountById queryIqAccount,
            CancellationToken cancellationToken)
        {
            var result = await _readStore.GetAsync(queryIqAccount.AccountId.Value, cancellationToken);

            return result.ReadModel?.ToIqAccount();
        }

        public async Task<IqAccount> ExecuteQueryAsync(QueryIqAccountByEmailAddress query, CancellationToken cancellationToken)
        {
            var result = await _readStore.FindAsync(x => x.UserName == new User(query.EmailAddress), cancellationToken);

            return result?.FirstOrDefault()?.ToIqAccount();
        }
    }

}