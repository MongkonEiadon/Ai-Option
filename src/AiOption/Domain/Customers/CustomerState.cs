using AiOption.Domain.Common;
using AiOption.Domain.Customers.Events;
using EventFlow.Aggregates;

namespace AiOption.Domain.Customers
{
    public class CustomerState : AggregateState<CustomerAggregate, CustomerId, CustomerState>,
        IApply<OpenAccount>
    {
        public User EmailAddress { get; private set; }
        public Password Password { get; private set; }
        public string InvitationCode { get; private set; }
        public string AuthorizeToken { get; private set; }


        public void Apply(OpenAccount aggregateEvent)
        {
            EmailAddress = aggregateEvent.UserName;
            Password = aggregateEvent.Password;
            InvitationCode = aggregateEvent.InvitationCode;
        }
    }
}