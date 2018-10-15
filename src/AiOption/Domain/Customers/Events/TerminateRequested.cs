using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events
{
    public class TerminateRequested : AggregateEvent<CustomerAggregate, CustomerId> { }
}