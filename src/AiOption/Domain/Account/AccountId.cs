using EventFlow.Core;

namespace AiOption.Domain.Account
{
    public class AccountId : Identity<AccountId> {
        public AccountId(string value) : base(value)
        {
        }
    }
}