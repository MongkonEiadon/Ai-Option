using AiOption.Domain.Customers;
using EventFlow.Queries;

namespace AiOption.Query.Customers
{
    public class GetCustomerByIdQuery : IQuery<Customer>
    {
        public CustomerId CustomerId { get; }
        public GetCustomerByIdQuery(CustomerId customerId)
        {
            CustomerId = customerId;
        }
    }
}