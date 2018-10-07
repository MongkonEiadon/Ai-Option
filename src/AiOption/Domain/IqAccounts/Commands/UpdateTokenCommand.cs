using EventFlow.Commands;

namespace AiOption.Domain.IqAccounts.Commands
{
    public class UpdateTokenCommand : Command<IqAccountAggregate, IqAccountId>
    {
        public UpdateTokenCommand(IqAccountId aggregateAccountId, string token) : base(aggregateAccountId)
        {
            Token = token;
        }

        public string Token { get; }
    }
}