using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events {

    public class CustomerRegisterFailed : AggregateEvent<CustomerAggregate, CustomerId> {

        public NewCustomer NewCustomer { get; }

        protected CustomerRegisterFailed(NewCustomer newCustomer) {
            NewCustomer = newCustomer;

        }

    }

}