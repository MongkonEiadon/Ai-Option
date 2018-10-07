using AiOption.Domain.Common;
using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events
{
    public class RequestChangeLevel : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public RequestChangeLevel(Level userLevel)
        {
            UserLevel = userLevel;
        }

        public Level UserLevel { get; }
    }
}