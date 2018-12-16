using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Query;
using AiOption.Query.Customers;
using EventFlow.Commands;
using EventFlow.Exceptions;
using EventFlow.Queries;
using EventFlow.ReadStores;
using EventFlow.ReadStores.InMemory;

namespace AiOption.Domain.Customers.Commands
{
    public class CustomerRegisterCommand : Command<CustomerAggregate, CustomerId>
    {
        public CustomerRegisterCommand(
            string emailAddress,
            string password,
            string invitationCode) : base(CustomerId.New)
        {
            EmailAddress = emailAddress;
            Password = password;
            InvitationCode = invitationCode;
        }

        public string EmailAddress { get; }
        public string Password { get; }
        public string InvitationCode { get; }
    }

    internal class CustomerRequestRegisterCommandHandler : CommandHandler<CustomerAggregate, CustomerId, CustomerRegisterCommand>
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ISearchableReadModelStore<CustomerReadModel> _readStore;

        public CustomerRequestRegisterCommandHandler(
            IQueryProcessor queryProcessor,
            ISearchableReadModelStore<CustomerReadModel> readStore)
        {
            _queryProcessor = queryProcessor;
            _readStore = readStore;
        }

        public override async Task ExecuteAsync(CustomerAggregate aggregate, CustomerRegisterCommand command,
            CancellationToken cancellationToken)
        {
            var query = new QueryEmailAddressExists(new Email(command.EmailAddress));
            if (await _queryProcessor.ProcessAsync(query, cancellationToken))
                throw DomainError.With($"UserName {command.EmailAddress} already exists.");

            aggregate.RegisterAnAccount(
                new Email(command.EmailAddress),
                new Password(command.Password),
                command.InvitationCode);
        }
    }
}