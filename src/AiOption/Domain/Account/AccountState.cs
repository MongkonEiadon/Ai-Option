using AiOption.Domain.Account.Events;
using EventFlow.Aggregates;

namespace AiOption.Domain.Account
{
    public class AccountState : AggregateState<AccountAggregate, AccountId, AccountState>,
        IApply<OpenAccount>
    {
        public string EmailAddress { get; private set; }
        public string Password { get; private set; }
        public string InvitationCode { get; private set; }
        public string AuthorizeToken { get; private set; }


        public void Apply(OpenAccount aggregateEvent)
        {
            EmailAddress = aggregateEvent.EmailAddress;
            Password = aggregateEvent.Password;
            InvitationCode = aggregateEvent.InvitationCode;
        }
    }
}