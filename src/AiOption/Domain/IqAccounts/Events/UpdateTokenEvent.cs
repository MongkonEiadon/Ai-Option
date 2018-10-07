using EventFlow.Aggregates;

namespace AiOption.Domain.IqAccounts.Events
{
    public class UpdateTokenEvent : AggregateEvent<IqAccountAggregate, IqAccountId>
    {
        public UpdateTokenEvent(string token)
        {
            Token = token;
        }

        public string Token { get; }
    }
}