using System;
using System.Collections.Generic;
using System.Text;

using AiOption.Domain.Accounts.Results;

using EventFlow.Commands;
using EventFlow.Core;

namespace AiOption.Domain.Accounts.Commands {

    public class LoginCommand : Command<IqAggregateRoot, IqIdentity, LoginCommandResult> {

        public string EmailAddress { get; }
        public string Password { get; }

        public LoginCommand(IqIdentity aggregateId, string emailAddress, string password) : base(aggregateId) {
            EmailAddress = emailAddress;
            Password = password;
        }
    }

}
