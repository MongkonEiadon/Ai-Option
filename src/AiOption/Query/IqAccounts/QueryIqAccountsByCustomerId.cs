using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Customers;
using AiOption.Domain.IqAccounts;
using EventFlow.Queries;

namespace AiOption.Query.IqAccounts
{
    public class QueryIqAccountsByCustomerId : IQuery<IReadOnlyCollection<IqAccount>>
    {
        public QueryIqAccountsByCustomerId(CustomerId customerId)
        {
            CustomerId = customerId;
        }

        public CustomerId CustomerId { get; }
    }

    internal class
        QueryIqAccountByCustomerIdQueryHandler : IQueryHandler<QueryIqAccountsByCustomerId,
            IReadOnlyCollection<IqAccount>>
    {
        private readonly ISearchableReadModelStore<IqAccountReadModel> _searchableReadModelStore;

        public QueryIqAccountByCustomerIdQueryHandler(
            ISearchableReadModelStore<IqAccountReadModel> searchableReadModelStore)
        {
            _searchableReadModelStore = searchableReadModelStore;
        }

        public async Task<IReadOnlyCollection<IqAccount>> ExecuteQueryAsync(QueryIqAccountsByCustomerId query,
            CancellationToken cancellationToken)
        {
            var result = await _searchableReadModelStore.FindAsync(x => x.CustomerId == query.CustomerId, cancellationToken);
            if (result.Any()) return result.Select(x => x.ToIqAccount()).ToList();

            return Enumerable.Empty<IqAccount>().ToList();
        }
    }
}