using AiOption.Domain.Common;

using EventFlow.Commands;

namespace AiOption.Domain.Customers.Commands {

    public class CustomerRegisterCommand : Command<CustomerAggregate, CustomerId> {

        public NewCustomer NewCustomer { get; }


        public CustomerRegisterCommand(CustomerId aggregateId, NewCustomer newCustomer) : base(aggregateId) {
            NewCustomer = newCustomer;

        }
    }

    

}