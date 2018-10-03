using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Customers;
using AiOption.Infrastructure.ReadStores.ReadModels;
using AiOption.Query.Customers;
using EventFlow.Queries;
using EventFlow.ReadStores;

namespace AiOption.Infrastructure.ReadStores.QueryHandlers.Customers
{
    class GetCustomerByIdQueryHandler : IQueryHandler<QueryCustomerById, Customer>
    {
        private readonly IReadModelStore<CustomerReadModel> _customerReadModelStore;

        public GetCustomerByIdQueryHandler(IReadModelStore<CustomerReadModel> customerReadModelStore)
        {
            _customerReadModelStore = customerReadModelStore;
        }

        public async Task<Customer> ExecuteQueryAsync(QueryCustomerById query, CancellationToken cancellationToken)
        {
            var result = await _customerReadModelStore.GetAsync(query.CustomerId.Value, cancellationToken);

            return result.ReadModel?.ToCustomer();
        }
    }
}