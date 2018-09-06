using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events {

    public class RegisteredDomainEvent : AggregateEvent<CustomerAggregate, CustomerId> {

        protected RegisteredDomainEvent() {
        }

    }

}