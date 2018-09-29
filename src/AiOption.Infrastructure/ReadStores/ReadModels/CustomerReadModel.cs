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
        IAmReadModelFor<CustomerAggregate, CustomerId, OpenAccount>
    {
        [Key] [Column("Id")] public string AggregateId { get; set;  }

        public User UserName { get; private set; }

        public Password Password { get; private set; }

        public string InvitationCode { get; private set; }

        public string EmailAddressNormalize => UserName.Value.ToUpper();


        public virtual ICollection<IqAccountReadModel> IqAccountReadModels { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, OpenAccount> domainEvent)
        {
            UserName = domainEvent.AggregateEvent.UserName;
            Password = domainEvent.AggregateEvent.Password;
            InvitationCode = domainEvent.AggregateEvent.InvitationCode;
        }

        public Customer ToAccount()
        {
            return new Customer(CustomerId.With(AggregateId), UserName, Password);
        }
    }
}
