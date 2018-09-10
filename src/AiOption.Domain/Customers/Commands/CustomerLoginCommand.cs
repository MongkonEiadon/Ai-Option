using System;
using System.Collections.Generic;
using System.Text;

using AiOption.Domain.Common;

using EventFlow.Commands;

namespace AiOption.Domain.Customers.Commands
{
    public class CustomerLoginCommand : Command<CustomerAggregate, CustomerId, CustomerLoginCommandResult> { 

        public string UserName { get; }
        public string PasswordHash { get; }

        public CustomerLoginCommand(CustomerId aggregateId, string userName, string passwordHash) : base(aggregateId) {
            UserName = userName;
            PasswordHash = passwordHash;
        }

    }


    public class CustomerLoginCommandResult : BaseResult {

        public string Message { get; }

        public CustomerLoginCommandResult(bool isSuccess, string message): base(isSuccess) {
            Message = message;

        }

    }


}
