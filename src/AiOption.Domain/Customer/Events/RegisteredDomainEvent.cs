using System.Collections.Generic;
using System.Text;
using EventFlow.Aggregates;

namespace AiOption.Domain.Customer.Events
{
    public class RegisteredDomainEvent : AggregateEvent<CustomerAggregateRoot, CustomerIdentity> {

        protected RegisteredDomainEvent()
        {
        }
    }

}

