using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events {

    public class RegisteredDomainEvent : AggregateEvent<CustomerAggregateRoot, CustomerIdentity> {

        protected RegisteredDomainEvent() {
        }

    }

}