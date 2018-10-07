using EventFlow.Core;

namespace AiOption.Domain.IqAccounts
{
    public class IqAccountId : Identity<IqAccountId>
    {
        public IqAccountId(string value) : base(value)
        {
        }
    }
}