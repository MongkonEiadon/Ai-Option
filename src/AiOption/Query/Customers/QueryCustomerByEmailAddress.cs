using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using EventFlow.Exceptions;
using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;

namespace AiOption.Query.Customers
{
    public class QueryCustomerByEmailAddress : IQuery<Customer>
    {
        public QueryCustomerByEmailAddress(Email emailAddress, bool throwIfNotFound = true)
        {
            EmailAddress = emailAddress;
            ThrowIfNotFound = throwIfNotFound;
        }

        public Email EmailAddress { get; }
        public bool ThrowIfNotFound { get; }
    }

    internal class QueryCustomerByEmailAddressQueryHandler : IQueryHandler<QueryCustomerByEmailAddress, Customer>
    {
        private readonly IInMemoryReadStore<CustomerReadModel> _customerInMemoryReadStore;
        private readonly ISearchableReadModelStore<CustomerReadModel> _readModelStore;

        public QueryCustomerByEmailAddressQueryHandler(ISearchableReadModelStore<CustomerReadModel> readModelStore,
            IInMemoryReadStore<CustomerReadModel> customerInMemoryReadStore)
        {
            _readModelStore = readModelStore;
            _customerInMemoryReadStore = customerInMemoryReadStore;
        }

        public Task<Customer> ExecuteQueryAsync(QueryCustomerByEmailAddress query, CancellationToken cancellationToken)
        {
            return _readModelStore.FindAsync(x => x.UserName == query.EmailAddress, cancellationToken)
                .ContinueWith(t =>
                {
                    if (query.ThrowIfNotFound && !t.Result.Any())
                        throw DomainError.With($"Not found customer by '{query.EmailAddress}'");
                    return t.Result?.FirstOrDefault()?.ToCustomer();
                }, cancellationToken);
        }
    }
}