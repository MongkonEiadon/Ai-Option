using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using EventFlow.Aggregates;

namespace AiOption.Domain.IqAccounts.Events
{
    public class RegisterNewAccountEvent : AggregateEvent<IqAccountAggregate, IqAccountId>
    {
        public RegisterNewAccountEvent(CustomerId customerId, User userName, Password password)
        {
            CustomerId = customerId;
            UserName = userName;
            Password = password;
        }

        public CustomerId CustomerId { get; }
        public User UserName { get; }
        public Password Password { get; }
    }
}