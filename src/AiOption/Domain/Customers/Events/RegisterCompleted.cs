using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events
{
    public class RegisterCompleted : AggregateEvent<CustomerAggregate, CustomerId>
    {
    }
}