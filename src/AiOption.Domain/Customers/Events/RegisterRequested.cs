using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events {

    public class RegisterRequested : AggregateEvent<CustomerAggregate, CustomerId> {

        public RegisterRequested(CustomerReadModel newCustomer) {
            NewCustomer = newCustomer;
        }

        public CustomerReadModel NewCustomer { get; }

    }

}