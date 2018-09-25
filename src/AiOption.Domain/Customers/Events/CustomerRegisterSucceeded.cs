using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events {

    public class CustomerRegisterSucceeded : AggregateEvent<CustomerAggregate, CustomerId> {

        public CustomerRegisterSucceeded(CustomerReadModel newCustomer) {
            NewCustomer = newCustomer;
        }

        public CustomerReadModel NewCustomer { get; }

    }

}