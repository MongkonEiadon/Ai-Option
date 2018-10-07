using AiOption.Domain.Common;
using EventFlow.Commands;

namespace AiOption.Domain.IqAccounts.Commands
{
    public class VerifyIqAccountCommand : Command<IqAccountAggregate, IqAccountId, VerifyIqAccountResult>
    {
        public VerifyIqAccountCommand(string emailAddress, string password) : base(IqAccountId.New)
        {
            EmailAddress = emailAddress;
            Password = password;
        }

        public string EmailAddress { get; }
        public string Password { get; }
    }
}