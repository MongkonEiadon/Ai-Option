﻿using AiOption.Domain.Common;
using EventFlow.Commands;

namespace AiOption.Domain.IqAccounts.Commands
{
    public class VerifyIqAccountCommand : Command<IqAccountAggregate, IqAccountId, VerifyIqAccountResult>
    {
        public string EmailAddress { get; }
        public string Password { get; }

        public VerifyIqAccountCommand(string emailAddress, string password) : base(IqAccountId.New)
        {
            EmailAddress = emailAddress;
            Password = password;
        }
      
    }
}