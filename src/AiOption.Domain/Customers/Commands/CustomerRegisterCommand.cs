using EventFlow.Commands;

namespace AiOption.Domain.Customers.Commands {

    public class CustomerRegisterCommand : Command<CustomerAggregate, CustomerId> {

        public CustomerRegisterCommand(CustomerId aggregateId, CustomerReadModel newCustomer) : base(aggregateId) {
            NewCustomer = newCustomer;

        }

        public CustomerReadModel NewCustomer { get; }

    }


}