using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace AiOption.Domain.Customers.Commands
{
    public class CustomerRegisterCompletedCommand : Command<CustomerAggregate, CustomerId>
    {
        public CustomerRegisterCompletedCommand(CustomerId aggregateId) : base(aggregateId)
        {
        }
    }

    internal class
        CustomerRegisterCompletedCommandHandler : CommandHandler<CustomerAggregate, CustomerId,
            CustomerRegisterCompletedCommand>
    {
        public override Task ExecuteAsync(CustomerAggregate aggregate, CustomerRegisterCompletedCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.CompletedRegister();

            return Task.CompletedTask;
        }
    }
}