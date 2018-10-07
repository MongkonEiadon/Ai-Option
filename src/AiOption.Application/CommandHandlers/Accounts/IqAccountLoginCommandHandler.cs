using System.Threading;
using System.Threading.Tasks;
using AiOption.Application.API;
using AiOption.Domain.IqAccounts;
using AiOption.Domain.IqAccounts.Commands;
using AiOption.Query.IqAccounts;
using EventFlow;
using EventFlow.Commands;
using EventFlow.Exceptions;
using EventFlow.Queries;

namespace AiOption.Application.CommandHandlers.Accounts
{
    internal class IqAccountLoginCommandHandler : CommandHandler<IqAccountAggregate, IqAccountId, IqAccountLoginCommand>
    {
        private readonly IIqOptionApiWrapper _apiWrapper;
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _query;

        public IqAccountLoginCommandHandler(IQueryProcessor query, IIqOptionApiWrapper apiWrapper,
            ICommandBus commandBus)
        {
            _query = query;
            _apiWrapper = apiWrapper;
            _commandBus = commandBus;
        }

        public override async Task ExecuteAsync(IqAccountAggregate aggregate, IqAccountLoginCommand command,
            CancellationToken cancellationToken)
        {
            // query existing account
            var account = await _query.ProcessAsync(new QueryIqAccountById(command.AggregateId), cancellationToken);

            // throw if not found
            if (account == null)
                throw DomainError.With($"Iq Account with {command.AggregateId} not found!");

            // verify iq-account to get token
            var result = await _commandBus.PublishAsync(
                new VerifyIqAccountCommand(account.UserName.Value, account.Password.DecryptPassword()),
                cancellationToken);

            // aggregate the events
            if (result.IsSuccess)
            {
                aggregate.LoginSucceeded();
                aggregate.ChangeToken(result.Message);
                return;
            }

            aggregate.LoginFailed(result.Message);
        }
    }
}