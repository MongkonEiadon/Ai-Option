using AiOption.Domain.Common;
using EventFlow.Entities;

namespace AiOption.Domain.IqOptions
{
    public class IqAccount : Entity<IqId>
    {
        public User UserName { get; }
        public Password Password { get; }

        public string SecuredToken { get; private set; }

        public IqAccount(IqId id, User userName, Password password) : base(id)
        {
            UserName = userName;
            Password = password;
        }

        public void SetSecuredToken(string token)
        {
            SecuredToken = token;
        }
    }
}