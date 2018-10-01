using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using EventFlow.Commands;
using EventFlow.Queries;

namespace AiOption.Domain.Customers.Commands
{
    public class ChangeLevelCommand : Command<CustomerAggregate, CustomerId> {
        public Level CustomerLevel { get; }

        public ChangeLevelCommand(CustomerId aggregateId, Level customerLevel) : base(aggregateId)
        {
            CustomerLevel = customerLevel;
        }
    }

    class CustomerChangeLevelCommandHandler : CommandHandler<CustomerAggregate, CustomerId, ChangeLevelCommand>
    {
        public override Task ExecuteAsync(CustomerAggregate aggregate, ChangeLevelCommand command, CancellationToken cancellationToken)
        {
            aggregate.ChangeLevel(command.CustomerLevel);
            return Task.CompletedTask;
        }
    }
}