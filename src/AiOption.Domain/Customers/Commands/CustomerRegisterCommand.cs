using EventFlow.Commands;

namespace AiOption.Domain.Customers.Commands {

    public class CustomerRegisterCommand : Command<CustomerAggregateRoote, CustomerIdentity> {

        public CustomerRegisterCommand(CustomerIdentity aggregateIdentity, CustomerReadModel newCustomer) : base(aggregateIdentity) {
            NewCustomer = newCustomer;

        }

        public CustomerReadModel NewCustomer { get; }

    }


}