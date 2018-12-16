using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using EventFlow.ReadStores;

namespace AiOption.Domain.IqAccounts.Commands
{
    public class TerminateIqAccountCommand : Command<IqAccountAggregate, IqAccountId>
    {
        public TerminateIqAccountCommand(IqAccountId aggregateId) : base(aggregateId)
        {
        }
    }

    public class
        TerminateIqAccountCommandHandler : CommandHandler<IqAccountAggregate, IqAccountId, TerminateIqAccountCommand>
    {
        private readonly IReadModelPopulator _readModelPopulator;

        public TerminateIqAccountCommandHandler(IReadModelPopulator readModelPopulator)
        {
            _readModelPopulator = readModelPopulator;
        }

        public override Task ExecuteAsync(IqAccountAggregate aggregate, TerminateIqAccountCommand command,
            CancellationToken cancellationToken)
        {
            return _readModelPopulator.DeleteAsync(command.AggregateId.Value, typeof(IqAccountReadModel),
                    cancellationToken)
                .ContinueWith(t =>
                {
                    if (t.IsCompleted) aggregate.Terminated();
                }, cancellationToken);
        }
    }
}