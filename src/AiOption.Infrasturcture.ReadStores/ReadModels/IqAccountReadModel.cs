using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AiOption.Domain.Common;
using AiOption.Domain.IqAccounts;
using AiOption.Domain.IqAccounts.Events;
using AiOption.Infrasturcture.ReadStores.ReadModels;
using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace AiOption.Infrastructure.ReadStores.ReadModels
{
    [Table("IqAccountReadModel")]
    public class IqAccountReadModel : IReadModel,
        IAmReadModelFor<IqAccountAggregate, IqAccountId, UpdateTokenEvent>
    {
        [Key] [Column("AccountId")] public string AggregateId { get; set; }

        public User UserName { get; set; }

        public Password Password { get; set; }

        public string IqOptionToken { get; set; }

        public DateTimeOffset TokenUpdatedDate { get; set; }

        public CustomerReadModel CustomerReadModel { get; set; }

        public void Apply(IReadModelContext context,
            IDomainEvent<IqAccountAggregate, IqAccountId, UpdateTokenEvent> domainEvent)
        {
            IqOptionToken = domainEvent.AggregateEvent.Token;
            TokenUpdatedDate = domainEvent.Timestamp;
        }


        public IqAccount ToIqAccount()
        {
            var iq = new IqAccount(
                IqAccountId.With(AggregateId),
                UserName,
                Password);

            iq.SetSecuredToken(IqOptionToken);
            return iq;
        }
    }
}