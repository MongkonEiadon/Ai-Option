using AiOption.Domain.IqAccounts.Events;
using EventFlow.Aggregates;

namespace AiOption.Domain.IqAccounts
{
    [AggregateName("IqAccount")]
    public class IqAccountAggregate : AggregateRoot<IqAccountAggregate, IqAccountId>
    {
        public IqAccountAggregate(IqAccountId accountId) : base(accountId)
        {
            Register(state);
        }

        private IqAggregateState state { get; } = new IqAggregateState();


        public void ChangeToken(string token)
        {
            Emit(new UpdateTokenEvent(token));
        }


        public void Terminated() => Emit(new TerminateIqAccountCompleted());
    }
}