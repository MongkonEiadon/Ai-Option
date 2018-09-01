using AiOption.Domain.Accounts.Results;

using EventFlow.Commands;

namespace AiOption.Domain.Accounts.Commands {

    public class LoginCommand : Command<IqAggregateRoot, IqIdentity, LoginCommandResult> {

        public LoginCommand(IqIdentity aggregateId, string emailAddress, string password) : base(aggregateId) {
            EmailAddress = emailAddress;
            Password = password;
        }

        public string EmailAddress { get; }
        public string Password { get; }

    }

}