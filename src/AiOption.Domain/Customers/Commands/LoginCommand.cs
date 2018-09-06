using System;
using System.Collections.Generic;
using System.Text;

using AiOption.Domain.Common;

using EventFlow.Commands;

namespace AiOption.Domain.Customers.Commands
{
    public class LoginCommand : Command<CustomerAggregate, CustomerId, BaseResult> { 

        public string UserName { get; }
        public string PasswordHash { get; }

        public LoginCommand(CustomerId aggregateId, string userName, string passwordHash) : base(aggregateId) {
            UserName = userName;
            PasswordHash = passwordHash;
        }

    }


}
