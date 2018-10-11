using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Domain.IqAccounts;
using EventFlow.Queries;

namespace AiOption.Query.IqAccounts
{
    public class QueryIqAccountByEmailAddress : IQuery<IqAccount>
    {
        public QueryIqAccountByEmailAddress(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public string EmailAddress { get; }
    }

    class QueryIqAccountByEmailAddressQueryHandler :
        IQueryHandler<QueryIqAccountByEmailAddress, IqAccount>
    {
        private readonly ISearchableReadModelStore<IqAccountReadModel> _readStore;

        public QueryIqAccountByEmailAddressQueryHandler(
            ISearchableReadModelStore<IqAccountReadModel> readStore)
        {
            _readStore = readStore;
        }
        

        public async Task<IqAccount> ExecuteQueryAsync(QueryIqAccountByEmailAddress query, CancellationToken cancellationToken)
        {
            var result = await _readStore.FindAsync(x => x.UserName == new User(query.EmailAddress), cancellationToken);

            return result?.FirstOrDefault()?.ToIqAccount();
        }
    }
}