using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events {

    public class RegisterRequested : AggregateEvent<CustomerAggregate, CustomerId> {

        public CustomerState NewCustomer { get; }

        public RegisterRequested(CustomerState newCustomer) {
            NewCustomer = newCustomer;
        }
    }

}