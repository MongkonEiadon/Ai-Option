using AiOption.Domain.Common;
using EventFlow.Aggregates;

namespace AiOption.Domain.IqAccounts.Events
{
    public class RegisterNewAccountEvent : AggregateEvent<IqAccountAggregate, IqAccountId> {
        public User UserName { get; }
        public Password Password { get; }

        public RegisterNewAccountEvent(User userName, Password password)
        {
            UserName = userName;
            Password = password;
        }
    }
}