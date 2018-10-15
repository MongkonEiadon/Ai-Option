using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using EventFlow.ReadStores;

namespace AiOption.Domain.Customers.Commands
{
    public class TerminateCustomerCommand : Command<CustomerAggregate, CustomerId>
    {
        public TerminateCustomerCommand(CustomerId aggregateId) : base(aggregateId)
        {
        }
    }
    class TerminateCustomerCommandHandler : CommandHandler<CustomerAggregate, CustomerId, TerminateCustomerCommand>
    {
        private readonly IReadModelPopulator _readModelPopulator;

        public TerminateCustomerCommandHandler(IReadModelPopulator readModelPopulator)
        {
            _readModelPopulator = readModelPopulator;
        }

        public override Task ExecuteAsync(CustomerAggregate aggregate, TerminateCustomerCommand command, CancellationToken cancellationToken)
        {
            return _readModelPopulator
                .DeleteAsync(command.AggregateId.Value, typeof(CustomerReadModel), cancellationToken)
                .ContinueWith(t =>
                {
                    if (t.IsCompleted)
                    {
                        aggregate.Terminated();
                    }
                }, cancellationToken);

        }
    }
}