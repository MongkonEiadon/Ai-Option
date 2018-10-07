using System;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.IqAccounts.Events;
using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace AiOption.Domain.IqAccounts
{
    public class IqAccountReadModel : IReadModel,
        IAmReadModelFor<IqAccountAggregate, IqAccountId, UpdateTokenEvent>
    {
        public virtual string AggregateId { get; set; }

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