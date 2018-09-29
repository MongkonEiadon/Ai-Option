using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events {

    public class CustomerRegisterFailed : AggregateEvent<CustomerAggregateRoote, CustomerIdentity> {

        public CustomerRegisterFailed(string failedMessage) {
            FailedMessage = failedMessage;
        }

        public string FailedMessage { get; }

    }


}