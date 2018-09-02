using AiOption.Domain.IqAccounts.Results;

using EventFlow.Commands;

namespace AiOption.Domain.IqAccounts.Commands {

    public class LoginCommand : Command<IqAggregate, IqIdentity, LoginCommandResult> {

        public LoginCommand(IqIdentity aggregateId, string emailAddress, string password) : base(aggregateId) {
            EmailAddress = emailAddress;
            Password = password;
        }

        public string EmailAddress { get; }
        public string Password { get; }

    }

}