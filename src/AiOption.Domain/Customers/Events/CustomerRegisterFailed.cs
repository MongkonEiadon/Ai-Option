using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events {

    public class CustomerRegisterFailed : AggregateEvent<CustomerAggregate, CustomerId> {

        public CustomerState NewCustomer { get; }

        protected CustomerRegisterFailed(CustomerState newCustomer) {
            NewCustomer = newCustomer;

        }

    }


}