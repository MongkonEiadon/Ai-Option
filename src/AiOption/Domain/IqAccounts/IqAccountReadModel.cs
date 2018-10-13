﻿using System;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using AiOption.Domain.Customers.Events;
using AiOption.Domain.IqAccounts.Events;
using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace AiOption.Domain.IqAccounts
{
    public partial class IqAccountReadModel : IReadModel,
        IAmReadModelFor<IqAccountAggregate, IqAccountId, UpdateTokenEvent>,
        IAmReadModelFor<CustomerAggregate, CustomerId, CreateNewIqAccountEvent>
    {
        public virtual string AggregateId { get; set; }

        public Email UserName { get; set; }

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

        private void UpdateReadModel(params Action<IqAccountReadModel>[] actions)
        {
            foreach (var action in actions)
            {
                action(this);
            }
        }

        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, CreateNewIqAccountEvent> domainEvent)
        {
            UpdateReadModel(
                x => x.CustomerId = domainEvent.AggregateIdentity,
                x => x.UserName = domainEvent.AggregateEvent.IqAccount.UserName,
                x => x.Password = domainEvent.AggregateEvent.IqAccount.Password);
        }
    }
}