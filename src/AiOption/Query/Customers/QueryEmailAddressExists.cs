using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;

namespace AiOption.Query.Customers
{
    public class QueryEmailAddressExists : IQuery<bool>
    {
        public Email EmailAddress { get; }
        public QueryEmailAddressExists(Email emailAddress)
        {
            EmailAddress = emailAddress;
        }
    }

    class QueryEmailAddressExistsHandler : IQueryHandler<QueryEmailAddressExists, bool>
    {
        private readonly ISearchableReadModelStore<CustomerReadModel> _readModelStore;

        public QueryEmailAddressExistsHandler(ISearchableReadModelStore<CustomerReadModel> readModelStore)
        {
            _readModelStore = readModelStore;
        }

        public Task<bool> ExecuteQueryAsync(QueryEmailAddressExists query, CancellationToken cancellationToken)
        {
            return _readModelStore.AnyAsync(x => x.UserName == query.EmailAddress, cancellationToken);
        }
    }
}