using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events {

    public class RegisterRequested : AggregateEvent<CustomerAggregateRoote, CustomerIdentity> {

        public RegisterRequested(CustomerReadModel newCustomer) {
            NewCustomer = newCustomer;
        }

        public CustomerReadModel NewCustomer { get; }

    }

}