using System;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.IqAccounts.Events;
using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace AiOption.Domain.IqAccounts
{
    public partial class IqAccountReadModel : IReadModel,
        IAmReadModelFor<IqAccountAggregate, IqAccountId, RegisterNewAccountEvent>,
        IAmReadModelFor<IqAccountAggregate, IqAccountId, UpdateTokenEvent>
    {
        public virtual string AggregateId { get; set; }

        public User UserName { get; set; }

        public Password Password { get; set; }

        public string IqOptionToken { get; set; }

        public DateTimeOffset TokenUpdatedDate { get; set; }
        
        public CustomerId CustomerId { get; set; }

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

        public void Apply(IReadModelContext context, IDomainEvent<IqAccountAggregate, IqAccountId, RegisterNewAccountEvent> domainEvent)
        {
            UpdateReadModel(
                x => x.CustomerId = domainEvent.AggregateEvent.CustomerId,
                x => x.UserName = domainEvent.AggregateEvent.UserName,
                x => x.Password = domainEvent.AggregateEvent.Password);
        }



        private void UpdateReadModel(params Action<IqAccountReadModel>[] actions)
        {
            foreach (var action in actions)
            {
                action(this);
            }
        }
    }
}