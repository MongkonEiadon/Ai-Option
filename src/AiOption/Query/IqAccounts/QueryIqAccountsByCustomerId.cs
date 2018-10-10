using AiOption.Domain.Customers;
using AiOption.Domain.IqAccounts;
using EventFlow.Queries;

namespace AiOption.Query.IqAccounts
{
    public class QueryIqAccountsByCustomerId : IQuery<IqAccount[]>
    {
        public CustomerId CustomerId { get; }

        public QueryIqAccountsByCustomerId(CustomerId customerId)
        {
            CustomerId = customerId;
        }
    }
}