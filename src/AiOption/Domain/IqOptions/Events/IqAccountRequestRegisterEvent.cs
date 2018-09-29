using AiOption.Domain.Common;
using EventFlow.Aggregates;

namespace AiOption.Domain.IqOptions.Events
{
    public class IqAccountRequestRegisterEvent : AggregateEvent<IqAggregate, IqId> {
        public User UserName { get; }
        public Password Password { get; }

        public IqAccountRequestRegisterEvent(User userName, Password password)
        {
            UserName = userName;
            Password = password;
        }
    }
}