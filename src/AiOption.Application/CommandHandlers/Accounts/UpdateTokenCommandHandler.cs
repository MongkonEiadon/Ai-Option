using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.IqAccounts;
using AiOption.Domain.IqAccounts.Commands;
using EventFlow.Commands;

namespace AiOption.Application.CommandHandlers.Accounts
{
    internal class UpdateTokenCommandHandler : CommandHandler<IqAccountAggregate, IqAccountId, UpdateTokenCommand>
    {
        public override Task ExecuteAsync(IqAccountAggregate aggregate, UpdateTokenCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.ChangeToken(command.Token);
            return Task.CompletedTask;
        }
    }
}