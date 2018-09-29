using AiOption.Domain.Accounts;
using AiOption.Domain.Accounts.Events;
using AiOption.Domain.Common;
using AiOption.Domain.Customers.Events;
using EventFlow.Aggregates;
using EventFlow.Extensions;

namespace AiOption.Domain.Customers
{
    [AggregateName("Customer")]
    public class CustomerAggregate : AggregateRoot<CustomerAggregate, CustomerId>
    {
        private readonly CustomerState state = new CustomerState();

        public CustomerAggregate(CustomerId id) : base(id)
        {
            Register(state);
        }

        #region Emitters

        public string EmailAddress { get; private set; }
        public string InvitationCode { get; private set; }

        public void RegisterAnAccount(User user, Password password, string invitationCode)
        {
            CustomerSpecs.NotCorrectEmailAddress
                .And(Specs.IsNew)
                .ThrowDomainErrorIfNotSatisfied(this);

            Emit(new OpenAccount(user, password, invitationCode));
        }
        

        #endregion
    }
}
