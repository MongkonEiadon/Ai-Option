using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AiOption.Domain.Customers.Events;

using EventFlow.Aggregates;
using EventFlow.ReadStores;

using Newtonsoft.Json;

namespace AiOption.Domain.Customers {

    public enum CustomerRegisterState {

        RegisterRequested = 0,
        Succeeded = 10,
        Failed = -10
    }
    
    [Table("ReadModel-CustomerReadModel")]
    public class CustomerReadModel : 
        IReadModel, //AggregateState<CustomerAggregate, CustomerId, CustomerReadModel>,
        IAmReadModelFor<CustomerAggregate, CustomerId, RegisterRequested>
    {

        public string EmailAddress { get; set; }


        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, RegisterRequested> domainEvent)
        {
            EmailAddress = domainEvent.AggregateEvent.NewCustomer.EmailAddress;
        }
    }

   

}