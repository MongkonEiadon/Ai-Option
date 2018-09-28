using AiOption.Domain.Command;
using EventFlow.Aggregates;

namespace AiOption.Domain.Account.Events
{
    public class AccountOpened : AggregateEvent<AccountAggregate, AccountId>
    {
        public AccountId Id { get; }
        public User User { get; }

        public AccountOpened(AccountId id, User user)
        {
            Id = id;
            User = user;
        }
    }
}