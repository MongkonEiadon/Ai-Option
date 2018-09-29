using AiOption.Domain.Accounts;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using EventFlow.Aggregates;

namespace AiOption.Domain.Accounts.Events
{
    public class CustomerRegistered : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public CustomerId Id { get; }
        public User User { get; }

        public CustomerRegistered(CustomerId id, User user)
        {
            Id = id;
            User = user;
        }
    }
}