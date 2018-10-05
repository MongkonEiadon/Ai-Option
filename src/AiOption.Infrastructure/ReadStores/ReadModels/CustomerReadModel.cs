using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AiOption.Domain.Accounts;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Events;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Microsoft.AspNetCore.Identity;

namespace AiOption.Infrastructure.ReadStores.ReadModels
{

    [Table("CustomerReadModel")]
    public class CustomerReadModel : 
        IReadModel,
        IAmReadModelFor<CustomerAggregate, CustomerId, RequestRegister>,
        IAmReadModelFor<CustomerAggregate, CustomerId, RequestChangeLevel>,
        IAmReadModelFor<CustomerAggregate, CustomerId, LoginSucceeded>,
        IAmReadModelFor<CustomerAggregate, CustomerId, CreateTokenSuccess>
    {
        [Key] [Column("AccountId")] public string AggregateId { get; set;  }

        public User UserName { get; private set; }

        public Password Password { get; private set; }

        public string InvitationCode { get; private set; }

        public Level Level { get; private set; }

        public DateTimeOffset LastLogin { get; private set; }

        public Token Token { get; private set; }



        public string EmailAddressNormalize => UserName.Value.ToUpper();

        public virtual ICollection<IqAccountReadModel> IqAccountReadModels { get; set; }

        public void Apply(
            IReadModelContext context, 
            IDomainEvent<CustomerAggregate, CustomerId, RequestRegister> domainEvent)
        {
            ApplyChanged(
                x => x.UserName = domainEvent.AggregateEvent.UserName,
                x => x.Password = domainEvent.AggregateEvent.Password,
                x => x.InvitationCode = domainEvent.AggregateEvent.InvitationCode);
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<CustomerAggregate, CustomerId, RequestChangeLevel> domainEvent) {
            ApplyChanged(x => x.Level = domainEvent.AggregateEvent.UserLevel);
        }

        public Customer ToCustomer() => new Customer(CustomerId.With(AggregateId), UserName, Password);
        

        #region [Privates]

         private void ApplyChanged(params Action<CustomerReadModel>[] paramActions)
        {
            foreach (var paramAction in paramActions)
            {
                paramAction(this);
            }
        }

        #endregion

        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, LoginSucceeded> domainEvent) {
            ApplyChanged(
                x => x.LastLogin = domainEvent.Timestamp);
        }

        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, CreateTokenSuccess> domainEvent) {
            ApplyChanged(x => x.Token = domainEvent.AggregateEvent.Token);
        }
    }
}
