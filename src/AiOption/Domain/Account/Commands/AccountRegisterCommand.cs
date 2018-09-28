using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Command;
using AiOption.Query.Account;
using EventFlow.Commands;
using EventFlow.Exceptions;
using EventFlow.Queries;

namespace AiOption.Domain.Account.Commands
{
    public class AccountRegisterCommand : Command<AccountAggregate, AccountId>
    {
        public string EmailAddress { get; }
        public string Password { get; }
        public string InvitationCode { get; }

        public AccountRegisterCommand(
            string emailAddress, 
            string password, 
            string invitationCode) : base(AccountId.New)
        {
            EmailAddress = emailAddress;
            Password = password;
            InvitationCode = invitationCode;
        }
    }

    class AccountRegisterCommandHandler : CommandHandler<AccountAggregate, AccountId, AccountRegisterCommand>
    {
        private readonly IQueryProcessor _queryProcessor;

        public AccountRegisterCommandHandler(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public override async Task ExecuteAsync(AccountAggregate aggregate, AccountRegisterCommand command, CancellationToken cancellationToken)
        {
            var query = new GetAccountByEmailAddressQuery(new User(command.EmailAddress), false);
            if (await _queryProcessor.ProcessAsync(query, cancellationToken) != null)
            {
                throw DomainError.With($"EmailAddress {command.EmailAddress} already exists.");
            }

            aggregate.RegisterAnAccount(
                command.EmailAddress,
                command.Password,
                command.InvitationCode);

        }
    }
}
