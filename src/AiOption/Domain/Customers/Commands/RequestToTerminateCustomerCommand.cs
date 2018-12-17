using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace AiOption.Domain.Customers.Commands
{
    public class RequestToTerminateCustomerCommand : Command<CustomerAggregate, CustomerId>
    {
        public RequestToTerminateCustomerCommand(CustomerId aggregateId) : base(aggregateId)
        {
        }
    }

    internal class
        TerminateRequestCommandHandler : CommandHandler<CustomerAggregate, CustomerId, RequestToTerminateCustomerCommand>
    {
        public override Task ExecuteAsync(CustomerAggregate aggregate, RequestToTerminateCustomerCommand customerCommand,
            CancellationToken cancellationToken)
        {
            aggregate.Terminate();
            return Task.CompletedTask;
        }
    }
}