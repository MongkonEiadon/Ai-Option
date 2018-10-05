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
        public User EmailAddress { get; private set; }
        public Password Password { get; private set; }
        public string InvitationCode { get; private set; }
        public Level Level { get; private set; }
        public DateTimeOffset LastLogin { get; private set; }
        public Token Token { get; private set; }


        private void ApplyChanged(params Action<CustomerState>[] actions)
        {
            foreach (var action in actions)
                action(this);
        }


        public void Apply(RequestRegister aggregateEvent) {
            ApplyChanged(
                x => x.EmailAddress = aggregateEvent.UserName,
                x => x.Password = aggregateEvent.Password,
                x => x.InvitationCode = aggregateEvent.InvitationCode);
        }

        public void Apply(RequestChangeLevel aggregateEvent) {
            ApplyChanged(x => x.Level = aggregateEvent.UserLevel);
        }


        public void Apply(LoginSucceeded aggregateEvent) {
            ApplyChanged(x => x.LastLogin = aggregateEvent.SuccessTime);
        }

        public void Apply(CreateTokenSuccess aggregateEvent) {
            ApplyChanged(x => x.Token = aggregateEvent.Token);
        }
    }
}