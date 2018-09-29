using System;
using System.Collections.Generic;
using System.Text;
using AiOption.Domain.Account.Events;
using EventFlow.Aggregates;
using EventFlow.Extensions;

namespace AiOption.Domain.Account
{
    [AggregateName("Account")]
    public class AccountAggregate : AggregateRoot<AccountAggregate, AccountId>
    {
        private readonly AccountState state = new AccountState();

        public AccountAggregate(AccountId id) : base(id)
        {
            Register(state);
        }

        #region Emitters

        public string EmailAddress { get; private set; }
        public string InvitationCode { get; private set; }

        public void RegisterAnAccount(string emailAddress, string password, string invitationCode)
        {
            AccountSpecs.NotCorrectEmailAddress
                .And(Specs.IsNew)
                .ThrowDomainErrorIfNotSatisfied(this);

            Emit(new OpenAccount(emailAddress, password, invitationCode));
        }
        

        #endregion
    }
}
