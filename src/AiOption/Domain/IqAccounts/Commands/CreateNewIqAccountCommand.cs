using AiOption.Domain.Common;
using EventFlow.Commands;

namespace AiOption.Domain.IqAccounts.Commands
{
    public class CreateNewIqAccountCommand : Command<IqAccountAggregate, IqAccountId>
    {
        public User User { get; }
        public Password Password { get; }
        public string Token { get; }

        public CreateNewIqAccountCommand(User user, Password password, string token) : base(IqAccountId.New)
        {
            User = user;
            Password = password;
            Token = token;
        }
    }
}