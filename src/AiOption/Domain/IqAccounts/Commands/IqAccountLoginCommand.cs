using EventFlow.Commands;

namespace AiOption.Domain.IqAccounts.Commands
{
    public class IqAccountLoginCommand : Command<IqAccountAggregate, IqAccountId>
    {
        public IqAccountLoginCommand(IqAccountId aggregateAccountId) : base(aggregateAccountId)
        {
        }
    }
}