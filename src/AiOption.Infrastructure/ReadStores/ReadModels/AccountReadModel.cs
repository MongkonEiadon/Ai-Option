using System;
using System.ComponentModel.DataAnnotations;
using AiOption.Domain.Account;
using AiOption.Domain.Account.Events;
using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace AiOption.Infrastructure.ReadStores.ReadModels
{
    public class AccountReadModel : IReadModel,
        IAmReadModelFor<AccountAggregate, AccountId, AccountOpened>
    {
        [Key] public string AggregateId { get; set;  }

        public string EmailAddress { get; set; }

        public string EmailAddressNormalize => EmailAddress.ToUpper();

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountOpened> domainEvent)
        {
            throw new NotImplementedException();
        }

        public Account ToAccount()
        {
            return new Account(AccountId.With(AggregateId), EmailAddress);
        }
    }
}
