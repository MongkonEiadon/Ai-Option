using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Common;
using AiOption.Domain.Customers.DomainServices;

using EventFlow.Commands;

namespace AiOption.Domain.Customers.Commands {

    public class BeginRegisterCustomerCommand : Command<CustomerAggregateRoote, CustomerIdentity, BaseResult> {

        public BeginRegisterCustomerCommand(CustomerIdentity aggregateIdentity, CustomerReadModel newCustomer) : base(aggregateIdentity) {
            NewCustomer = newCustomer;
        }

        public CustomerReadModel NewCustomer { get; }

    }


   
}