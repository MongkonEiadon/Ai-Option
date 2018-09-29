using System;
using System.Collections.Generic;
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
    
    public class CustomerReadModel : 
        IReadModel, //AggregateState<CustomerAggregateRoote, CustomerIdentity, CustomerReadModel>,
        IAmReadModelFor<CustomerAggregateRoote, CustomerIdentity, RegisterRequested>
    {

        public string EmailAddress { get; set; }


        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregateRoote, CustomerIdentity, RegisterRequested> domainEvent)
        {
            EmailAddress = domainEvent.AggregateEvent.NewCustomer.EmailAddress;
        }
    }

   

}