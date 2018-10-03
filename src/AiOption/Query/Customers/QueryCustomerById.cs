using AiOption.Domain.Customers;
using EventFlow.Queries;

namespace AiOption.Query.Customers
{
    public class QueryCustomerById : IQuery<Customer>
    {
        public CustomerId CustomerId { get; }
        public QueryCustomerById(CustomerId customerId)
        {
            CustomerId = customerId;
        }
    }
}