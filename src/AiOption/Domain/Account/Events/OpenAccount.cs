using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Aggregates;

namespace AiOption.Domain.Account.Events
{
    public class OpenAccount : AggregateEvent<AccountAggregate, AccountId>
    {
        public string EmailAddress { get; }
        public string Password { get; }
        public string InvitationCode { get; }

        public OpenAccount(
            string emailAddress, 
            string password, 
            string invitationCode)
        {
            EmailAddress = emailAddress;
            Password = password;
            InvitationCode = invitationCode;
        }
    }
}
