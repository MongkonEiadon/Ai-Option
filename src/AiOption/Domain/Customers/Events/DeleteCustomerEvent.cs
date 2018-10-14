using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events
{
    public class DeleteCustomerEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
    }
}