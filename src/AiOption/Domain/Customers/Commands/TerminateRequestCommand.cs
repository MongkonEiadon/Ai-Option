using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using EventFlow.ReadStores;

namespace AiOption.Domain.Customers.Commands
{
    public class TerminateRequestCommand : Command<CustomerAggregate, CustomerId>
    {
        public TerminateRequestCommand(CustomerId aggregateId) : base(aggregateId)
        {
        }
    }

    class TerminateRequestCommandHandler : CommandHandler<CustomerAggregate, CustomerId, TerminateRequestCommand>
    {
        public override Task ExecuteAsync(CustomerAggregate aggregate, TerminateRequestCommand requestCommand,
            CancellationToken cancellationToken)
        {
            aggregate.Terminate();
            return Task.CompletedTask;
        }
    }
}