using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;

using EventFlow.Commands;

namespace AiOption.Application.CommandHandlers.Customers
{
    public class CustomerAuthorizationCommandHandler
        : ICommandHandler<CustomerAggregate, CustomerId, CustomerLoginCommandResult, LoginCommand>
    {

        public CustomerAuthorizationCommandHandler() {

        }

        public Task<CustomerLoginCommandResult> ExecuteCommandAsync(CustomerAggregate aggregate, LoginCommand command, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

    }
}
