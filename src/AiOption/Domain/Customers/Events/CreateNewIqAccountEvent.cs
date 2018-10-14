using AiOption.Domain.IqAccounts;
using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events
{
    public class CreateNewIqAccountEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public CreateNewIqAccountEvent(IqAccount iqAccount)
        {
            IqAccount = iqAccount;
        }

        public IqAccount IqAccount { get; }
    }
}