using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Accounts;
using AiOption.Domain.Common;
using AiOption.Query.Customers;
using EventFlow.Commands;
using EventFlow.Exceptions;
using EventFlow.Queries;

namespace AiOption.Domain.Customers.Commands
{
    public class CustomerRequestRegisterCommand : Command<CustomerAggregate, CustomerId>
    {
        public string EmailAddress { get; }
        public string Password { get; }
        public string InvitationCode { get; }

        public CustomerRequestRegisterCommand(
            string emailAddress, 
            string password, 
            string invitationCode) : base(CustomerId.New)
        {
            EmailAddress = emailAddress;
            Password = password;
            InvitationCode = invitationCode;
        }
    }

    class CustomerRequestRegisterCommandHandler : CommandHandler<CustomerAggregate, CustomerId, CustomerRequestRegisterCommand>
    {
        private readonly IQueryProcessor _queryProcessor;

        public CustomerRequestRegisterCommandHandler(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public override async Task ExecuteAsync(CustomerAggregate aggregate, CustomerRequestRegisterCommand command, CancellationToken cancellationToken)
        {
            var query = new GetCustomerByEmailAddressQuery(new User(command.EmailAddress), false);
            if (await _queryProcessor.ProcessAsync(query, cancellationToken) != null)
            {
                throw DomainError.With($"UserName {command.EmailAddress} already exists.");
            }

            aggregate.RegisterAnAccount(
                new User(command.EmailAddress), 
                new Password(command.Password),
                command.InvitationCode);

        }
    }
}
