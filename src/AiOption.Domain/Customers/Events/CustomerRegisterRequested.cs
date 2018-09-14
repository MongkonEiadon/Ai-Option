using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events {

    public class CustomerRegisterRequested : AggregateEvent<CustomerAggregate, CustomerId> {

        public CustomerState NewCustomer { get; }

        public CustomerRegisterRequested(CustomerState newCustomer) {
            NewCustomer = newCustomer;
        }
    }

}