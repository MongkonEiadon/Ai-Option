using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events
{
    public class TerminateCustomerCompleted :AggregateEvent<CustomerAggregate, CustomerId> { }
}