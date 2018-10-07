using AiOption.Domain.Common;
using EventFlow.Aggregates;

namespace AiOption.Domain.IqAccounts.Events
{
    public class RegisterNewAccountEvent : AggregateEvent<IqAccountAggregate, IqAccountId>
    {
        public RegisterNewAccountEvent(User userName, Password password)
        {
            UserName = userName;
            Password = password;
        }

        public User UserName { get; }
        public Password Password { get; }
    }
}