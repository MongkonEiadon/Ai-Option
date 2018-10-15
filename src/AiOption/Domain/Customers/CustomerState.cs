using System.Collections.Generic;
using AiOption.Domain.Customers.Events;
using EventFlow.Aggregates;

namespace AiOption.Domain.Customers
{
    public class CustomerState : AggregateState<CustomerAggregate, CustomerId, CustomerState>,
        IApply<RequestRegister>,
        IApply<RequestChangeLevel>,
        IApply<LoginSucceeded>,
        IApply<CreateTokenSuccess>,
        IApply<CreateNewIqAccountEvent>,
        IApply<DeleteCustomerEvent>,

        IApply<TerminateRequested>,
        IApply<TerminateCustomerCompleted>
    {
        private readonly List<CustomerStatus> _status = new List<CustomerStatus>();
        public IReadOnlyCollection<CustomerStatus> Status => _status;

        public void Apply(CreateNewIqAccountEvent aggregateEvent)
        {
            _status.Add(CustomerStatus.AddIqAccount);
        }


        public void Apply(CreateTokenSuccess aggregateEvent)
        {
            _status.Add(CustomerStatus.RegisterSucceeded);
        }

        public void Apply(DeleteCustomerEvent aggregateEvent)
        {
            _status.Add(CustomerStatus.Deleted);
        }


        public void Apply(LoginSucceeded aggregateEvent)
        {
            _status.Add(CustomerStatus.LoggedIn);
        }

        public void Apply(RequestChangeLevel aggregateEvent)
        {
            _status.Add(CustomerStatus.ChangeLevel);
        }


        public void Apply(RequestRegister aggregate)
        {
            _status.Add(CustomerStatus.Register);
        }

        public void LoadState(IEnumerable<CustomerStatus> status)
        {
            _status.AddRange(status);
        }

        public void Apply(TerminateRequested aggregateEvent)
        {

        }

        public void Apply(TerminateCustomerCompleted aggregateEvent)
        {
        }
    }
}