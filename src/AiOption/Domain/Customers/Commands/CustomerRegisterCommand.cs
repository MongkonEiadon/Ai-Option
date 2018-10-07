using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Query.Customers;
using EventFlow.Commands;
using EventFlow.Exceptions;
using EventFlow.Queries;

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

        public CustomerRequestRegisterCommandHandler(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public override async Task ExecuteAsync(CustomerAggregate aggregate, CustomerRegisterCommand command,
            CancellationToken cancellationToken)
        {
            var query = new QueryCustomerByEmailAddress(new User(command.EmailAddress), false);
            if (await _queryProcessor.ProcessAsync(query, cancellationToken) != null)
                throw DomainError.With($"UserName {command.EmailAddress} already exists.");

            aggregate.RegisterAnAccount(
                new User(command.EmailAddress),
                new Password(command.Password),
                command.InvitationCode);
        }
    }
}