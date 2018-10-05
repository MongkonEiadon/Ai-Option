using AiOption.Domain.Customers;
using EventFlow.Queries;

namespace AiOption.Query.Customers
{
    public class QueryCustomerById : IQuery<Customer>
    {
        public CustomerId CustomerId { get; }
        public bool ThrowIfNotFound { get; }

        public QueryCustomerById(CustomerId customerId, bool throwIfNotFound = true)
        {
            CustomerId = customerId;
            ThrowIfNotFound = throwIfNotFound;
        }
    }
}