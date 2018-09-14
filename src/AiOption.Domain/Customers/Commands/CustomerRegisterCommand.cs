using EventFlow.Commands;
using EventFlow.Core;

namespace AiOption.Domain.Customers.Commands {

    public class CustomerRegisterCommand : Command<CustomerAggregate, CustomerId> {

        public CustomerState NewCustomer { get; }


        public CustomerRegisterCommand(CustomerId aggregateId, CustomerState newCustomer) : base(aggregateId) {
            NewCustomer = newCustomer;

        }
    }


}