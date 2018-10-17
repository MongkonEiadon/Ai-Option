using System;
using AiOption.Domain.IqAccounts.Events;
using EventFlow.Aggregates;

namespace AiOption.Domain.IqAccounts
{
    public class IqAggregateState : AggregateState<IqAccountAggregate, IqAccountId, IqAggregateState>,
        IApply<UpdateTokenEvent>,
        IApply<TerminateIqAccountCompleted>
    {
        public string SecuredToken { get; private set; }


        public void Apply(UpdateTokenEvent aggregateEvent)
        {
            ApplyChanged(x => x.SecuredToken = aggregateEvent.Token);
        }

        private void ApplyChanged(params Action<IqAggregateState>[] actions)
        {
            foreach (var action in actions)
                action(this);
        }

        public void Apply(TerminateIqAccountCompleted aggregateEvent)
        {
        }
    }
}