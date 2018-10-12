using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Domain.IqAccounts;
using AiOption.Query.IqAccounts;
using EventFlow.Commands;
using EventFlow.Exceptions;
using EventFlow.Queries;

namespace AiOption.Domain.Customers.Commands
{
    public class CreateNewIqAccountCommand : Command<CustomerAggregate, CustomerId>
    {
        public User User { get; }
        public Password Password { get; }

        public CreateNewIqAccountCommand(CustomerId customerId, User user, Password password) : base(customerId)
        {
            User = user;
            Password = password;
        }
    }

    class CreateNewIqAccountCommandHandler : CommandHandler<CustomerAggregate, CustomerId, CreateNewIqAccountCommand>
    {
        private readonly IQueryProcessor _queryProcessor;
        public CreateNewIqAccountCommandHandler(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public override async Task ExecuteAsync(CustomerAggregate aggregate, CreateNewIqAccountCommand command, CancellationToken cancellationToken)
        {
            var query = new QueryIqAccountByEmailAddress(command.User.Value);
            var result = await _queryProcessor.ProcessAsync(query, cancellationToken);

            if (result != null)
                throw DomainError.With($"IqAccounts with {command.User} already exists in database!");

            var iqAccount = new IqAccount(IqAccountId.New, command.User, command.Password);
            aggregate.CreateNewIqAccount(iqAccount);
        }
    }
}