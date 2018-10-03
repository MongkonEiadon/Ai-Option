using EventFlow.Commands;

namespace AiOption.Domain.IqAccounts.Commands
{
    public class UpdateTokenCommand : Command<IqAccountAggregate, IqAccountId>
    {
        public string Token { get; }
        public UpdateTokenCommand(IqAccountId aggregateAccountId, string token) : base(aggregateAccountId)
        {
            Token = token;
        }
    }
}