using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events {

    public class CustomerRegisterSucceeded : AggregateEvent<CustomerAggregate, CustomerId> {

        public NewCustomer NewCustomer { get; }

        protected CustomerRegisterSucceeded(NewCustomer newCustomer) {
            NewCustomer = newCustomer;
        }

    }

}