using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events {

    public class RegisteredDomainEvent : AggregateEvent<CustomersAggregate, CustomerIdentity> {

        protected RegisteredDomainEvent() {
        }

    }

}