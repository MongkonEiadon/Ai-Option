using AiOption.Domain.IqAccounts.Events;

using EventFlow.Aggregates;

namespace AiOption.Domain.IqAccounts {

    public class IqAggregateState : AggregateState<IqAggregate, IqIdentity, IqAggregateState>,
        IApply<IqAccountLoginFailed> {

        public string EmailAddress { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }

        public void Apply(IqAccountLoginFailed aggregateEvent) {
            EmailAddress = aggregateEvent.EmailAddress;
            Message = aggregateEvent.Message;
            IsSuccess = false;
        }

    }

}