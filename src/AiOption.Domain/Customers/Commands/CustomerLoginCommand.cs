using System;
using System.Collections.Generic;
using System.Text;

using AiOption.Domain.Common;

using EventFlow.Commands;

namespace AiOption.Domain.Customers.Commands
{
    public class CustomerLoginCommand : Command<CustomerAggregate, CustomerId> { 

        public string UserName { get; }
        public string PasswordHash { get; }

        public CustomerLoginCommand(CustomerId aggregateId, string userName, string passwordHash) : base(aggregateId) {
            UserName = userName;
            PasswordHash = passwordHash;
        }

    }

}
