using System;
using System.Collections.Generic;
using AiOption.Domain.Common;
using AiOption.Domain.Customers.Events;
using AiOption.Domain.IqAccounts;
using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace AiOption.Domain.Customers
{
    public class CustomerReadModel : 
        IReadModel,
        IAmReadModelFor<CustomerAggregate, CustomerId, CreateTokenSuccess>,
        IAmReadModelFor<CustomerAggregate, CustomerId, LoginSucceeded>,
        IAmReadModelFor<CustomerAggregate, CustomerId, RequestChangeLevel>,
        IAmReadModelFor<CustomerAggregate, CustomerId, RequestRegister>
    {
        public virtual string AggregateId { get; set; }

        public Email UserName { get; private set; }

        public Password Password { get; private set; }

        public string InvitationCode { get; private set; }

        public Level Level { get; private set; }

        public DateTimeOffset LastLogin { get; private set; }

        public Token Token { get; private set; }

        public string EmailAddressNormalize => UserName.Value.ToUpper();



        public void Apply(IReadModelContext context,
            IDomainEvent<CustomerAggregate, CustomerId, CreateTokenSuccess> domainEvent)
        {
            ApplyChanged(x => x.Token = domainEvent.AggregateEvent.Token);
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<CustomerAggregate, CustomerId, LoginSucceeded> domainEvent)
        {
            ApplyChanged(
                x => x.LastLogin = domainEvent.Timestamp);
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<CustomerAggregate, CustomerId, RequestChangeLevel> domainEvent)
        {
            ApplyChanged(x => x.Level = domainEvent.AggregateEvent.UserLevel);
        }

        public void Apply(
            IReadModelContext context,
            IDomainEvent<CustomerAggregate, CustomerId, RequestRegister> domainEvent)
        {
            ApplyChanged(
                x => x.UserName = domainEvent.AggregateEvent.UserName,
                x => x.Password = domainEvent.AggregateEvent.Password,
                x => x.InvitationCode = domainEvent.AggregateEvent.InvitationCode);
        }

        public Customer ToCustomer()
        {
            return new Customer(CustomerId.With(AggregateId), UserName, Password);
        }


        #region [Privates]

        private void ApplyChanged(params Action<CustomerReadModel>[] paramActions)
        {
            foreach (var paramAction in paramActions) paramAction(this);
        }

        #endregion
    }
}
