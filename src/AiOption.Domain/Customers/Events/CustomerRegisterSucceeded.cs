using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events {

    public class CustomerRegisterSucceeded : AggregateEvent<CustomerAggregateRoote, CustomerIdentity> {

        public CustomerRegisterSucceeded(CustomerReadModel newCustomer) {
            NewCustomer = newCustomer;
        }

        public CustomerReadModel NewCustomer { get; }

    }

}