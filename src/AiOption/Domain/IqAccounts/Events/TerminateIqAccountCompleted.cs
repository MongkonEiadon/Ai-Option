using EventFlow.Aggregates;

namespace AiOption.Domain.IqAccounts.Events
{
    public class TerminateIqAccountCompleted : AggregateEvent<IqAccountAggregate, IqAccountId> { }
}