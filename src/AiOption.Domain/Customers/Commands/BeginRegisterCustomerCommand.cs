using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Common;
using AiOption.Domain.Customers.DomainServices;

using EventFlow.Commands;

namespace AiOption.Domain.Customers.Commands {

    public class BeginRegisterCustomerCommand : Command<CustomerAggregate, CustomerId, BaseResult> {

        public BeginRegisterCustomerCommand(CustomerId aggregateId, CustomerReadModel newCustomer) : base(aggregateId) {
            NewCustomer = newCustomer;
        }

        public CustomerReadModel NewCustomer { get; }

    }


   
}