using System;
using AiOption.Domain.Common;
using AiOption.Domain.Customers.Events;
using EventFlow.Aggregates;

namespace AiOption.Domain.Customers
{
    public class CustomerState : AggregateState<CustomerAggregate, CustomerId, CustomerState>,
        IApply<RequestRegister>,
        IApply<RequestChangeLevel>,
        IApply<LoginSucceeded>,
        IApply<CreateTokenSuccess>
    {
        public CustomerStatus Status { get;  private set; } = CustomerStatus.Undefined;

        public void Apply(CreateTokenSuccess aggregateEvent)
        {
            Status = CustomerStatus.RegisterSucceeded;
        }


        public void Apply(LoginSucceeded aggregateEvent)
        {
        }

        public void Apply(RequestChangeLevel aggregateEvent)
        {
        }


        public void Apply(RequestRegister aggregate)
        {
           
        }


        private void ApplyChanged(params Action<CustomerState>[] actions)
        {
            foreach (var action in actions)
                action(this);
        }
    }
}