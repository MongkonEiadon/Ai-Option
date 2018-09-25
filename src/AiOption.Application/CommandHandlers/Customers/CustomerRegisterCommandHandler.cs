using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;

using EventFlow.Commands;

namespace AiOption.Application.CommandHandlers.Customers {

    public class CustomerRegisterCommandHandler :
        CommandHandler<CustomerAggregate, CustomerId, CustomerRegisterCommand> {

        public override Task ExecuteAsync(CustomerAggregate aggregate, CustomerRegisterCommand command,
            CancellationToken cancellationToken) {

            var newCustomer = command.NewCustomer;

            aggregate.CustomerRegisterRequested(newCustomer);

            return Task.CompletedTask;
        }

    }


}