using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events {

    public class CustomerRegistered : AggregateEvent<CustomerAggregate, CustomerId> {

        protected CustomerRegistered() {
        }

    }


}