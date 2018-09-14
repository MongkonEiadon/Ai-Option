using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Application.ApplicationServices;
using AiOption.Application.Repositories.ReadOnly;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using AiOption.Domain.Customers.DomainServices;

using AutoMapper;

using EventFlow.Commands;

namespace AiOption.Application.CommandHandlers.Customers {

    public class CustomerRegisterCommandHandler :
        CommandHandler<CustomerAggregate, CustomerId, CustomerRegisterCommand> {

        public override Task ExecuteAsync(CustomerAggregate aggregate, CustomerRegisterCommand command, CancellationToken cancellationToken) {

            var newCustomer = command.NewCustomer;
            newCustomer.Id = aggregate.Id.GetGuid();

            aggregate.CustomerRegisterRequested(newCustomer);

            return Task.CompletedTask;
        }

    }



}
