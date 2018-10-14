using AiOption.Domain.Common;
using EventFlow.Entities;

namespace AiOption.Domain.IqAccounts
{
    public enum AccountType
    {
        Trader,
        Follower
    }

    public class IqAccount : Entity<IqAccountId>
    {
        public IqAccount(IqAccountId accountId, Email userName, Password password) : base(accountId)
        {
            UserName = userName;
            Password = password;
        }

        public Email UserName { get; set; }
        public Password Password { get; set; }
        public string SecuredToken { get; set; }
        public AccountType Type { get; set; }
    }
}