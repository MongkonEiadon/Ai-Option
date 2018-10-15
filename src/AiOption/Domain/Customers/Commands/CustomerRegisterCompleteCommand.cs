using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace AiOption.Domain.Customers.Commands
{
    public class CustomerRegisterCompleteCommand : Command<CustomerAggregate, CustomerId>
    {
        public CustomerRegisterCompleteCommand(CustomerId aggregateId) : base(aggregateId)
        {
        }
    }

    internal class
        CustomerRegisterCompleteCommandHandler : CommandHandler<CustomerAggregate, CustomerId,
            CustomerRegisterCompleteCommand>
    {
        public override Task ExecuteAsync(CustomerAggregate aggregate, CustomerRegisterCompleteCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.CompletedRegister();

            return Task.CompletedTask;
        }
    }
}