using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Customers;
using EventFlow.Exceptions;
using EventFlow.Queries;

namespace AiOption.Query.Customers
{
    public class QueryCustomerById : IQuery<Customer>
    {
        public QueryCustomerById(CustomerId customerId, bool throwIfNotFound = true)
        {
            CustomerId = customerId;
            ThrowIfNotFound = throwIfNotFound;
        }

        public CustomerId CustomerId { get; }
        public bool ThrowIfNotFound { get; }
    }

    class QueryCustomerByIdQueryHandler : IQueryHandler<QueryCustomerById, Customer>
    {
        private readonly ISearchableReadModelStore<CustomerReadModel> _readStore;
        public QueryCustomerByIdQueryHandler(ISearchableReadModelStore<CustomerReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<Customer> ExecuteQueryAsync(QueryCustomerById query, CancellationToken cancellationToken)
        {
            var result = await _readStore.GetAsync(query.CustomerId.Value, cancellationToken);
            if(result.ReadModel == null && query.ThrowIfNotFound) 
                throw DomainError.With($"Not found customer with '{query.CustomerId}'");

            return result.ReadModel?.ToCustomer();
        }
    }
}