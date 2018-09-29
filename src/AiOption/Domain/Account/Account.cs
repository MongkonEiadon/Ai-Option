using EventFlow.Entities;

namespace AiOption.Domain.Account
{
    public class Account : Entity<AccountId>
    {
        public string EmailAddress { get; }

        public Account(AccountId id, string emailAddress) : base(id)
        {
            EmailAddress = emailAddress;
        }
    }
}