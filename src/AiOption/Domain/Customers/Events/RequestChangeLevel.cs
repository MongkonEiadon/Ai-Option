using AiOption.Domain.Common;
using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events
{
    public class RequestChangeLevel : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public Level UserLevel { get; }
        public RequestChangeLevel(Level userLevel)
        {
            UserLevel = userLevel;
        }
    }
}