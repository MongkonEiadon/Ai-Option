using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Customers;
using AiOption.Infrasturcture.ReadStores.ReadModels;
using AiOption.Query.Customers;
using EventFlow.Exceptions;
using EventFlow.Queries;
using EventFlow.ReadStores;

namespace AiOption.Infrasturcture.ReadStores.QueryHandlers.Customers
{
    internal class GetCustomerByIdQueryHandler : IQueryHandler<QueryCustomerById, Customer>
    {
        private readonly IReadModelStore<CustomerReadModelDto> _customerReadModelStore;

        public GetCustomerByIdQueryHandler(IReadModelStore<CustomerReadModelDto> customerReadModelStore)
        {
            _customerReadModelStore = customerReadModelStore;
        }

        public async Task<Customer> ExecuteQueryAsync(QueryCustomerById query, CancellationToken cancellationToken)
        {
            var result = await _customerReadModelStore.GetAsync(query.CustomerId.Value, cancellationToken);

            if (result.ReadModel == null && query.ThrowIfNotFound)
                throw DomainError.With($"Not found customer with id : {query.CustomerId}");

            return result.ReadModel?.ToCustomer();
        }
    }
}