using AiOption.Domain.Common;
using EventFlow.Entities;

namespace AiOption.Domain.IqAccounts
{
    public class IqAccount : Entity<IqAccountId>
    {
        public IqAccount(IqAccountId accountId, User userName, Password password) : base(accountId)
        {
            UserName = userName;
            Password = password;
        }

        public User UserName { get; }
        public Password Password { get; }
        public string SecuredToken { get; private set; }

        public void SetSecuredToken(string token)
        {
            SecuredToken = token;
        }
    }
}