using System;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Query.IqAccounts;
using EventFlow.Commands;
using EventFlow.Exceptions;
using EventFlow.Queries;

namespace AiOption.Domain.IqAccounts.Commands
{
    public class CreateNewIqAccountCommand : Command<IqAccountAggregate, IqAccountId>
    {
        public CustomerId CustomerId { get; }
        public User User { get; }
        public Password Password { get; }
        public string Token { get; }

        public CreateNewIqAccountCommand(CustomerId customerId, User user, Password password, string token) : base(IqAccountId.New)
        {
            CustomerId = customerId;
            User = user;
            Password = password;
            Token = token;
        }
    }

    internal class CreateNewIqAccountCommandHandler : CommandHandler<IqAccountAggregate, IqAccountId, CreateNewIqAccountCommand>
    {
        private readonly IQueryProcessor _queryProcessor;
        public CreateNewIqAccountCommandHandler(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public override async Task ExecuteAsync(IqAccountAggregate aggregate, CreateNewIqAccountCommand command,
            CancellationToken cancellationToken)
        {

            var query = await _queryProcessor.ProcessAsync(new QueryIqAccountByEmailAddress(command.User.Value),
                cancellationToken);

            if (query != null)
                throw DomainError.With($"'{command.User}' already exists!");


            aggregate.CreateNewIqAccounts(command.CustomerId, command.User, command.Password);

            aggregate.ChangeToken(command.Token);
        }
    }
}