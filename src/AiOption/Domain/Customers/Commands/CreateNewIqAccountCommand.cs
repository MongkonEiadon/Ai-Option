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
        public CreateNewIqAccountCommand(CustomerId customerId, Email emailAddress, Password password) : base(
            customerId)
        {
            EmailAddress = emailAddress;
            Password = password;
        }

        public Email EmailAddress { get; }
        public Password Password { get; }
    }

    internal class
        CreateNewIqAccountCommandHandler : CommandHandler<CustomerAggregate, CustomerId, CreateNewIqAccountCommand>
    {
        private readonly IQueryProcessor _queryProcessor;

        public CreateNewIqAccountCommandHandler(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public override async Task ExecuteAsync(CustomerAggregate aggregate, CreateNewIqAccountCommand command,
            CancellationToken cancellationToken)
        {
            var query = new QueryIqAccountByEmailAddress(command.EmailAddress.Value);
            var result = await _queryProcessor.ProcessAsync(query, cancellationToken);

            if (result != null)
                throw DomainError.With($"IqAccounts with {command.EmailAddress} already exists in database!");

            var iqAccount = new IqAccount(IqAccountId.New, command.EmailAddress, command.Password);
            aggregate.CreateNewIqAccount(iqAccount);
        }
    }
}