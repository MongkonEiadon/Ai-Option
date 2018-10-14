using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using EventFlow.ReadStores;

namespace AiOption.Domain.Customers.Commands
{
    public class DeleteCustomerCommand : Command<CustomerAggregate, CustomerId>
    {
        public DeleteCustomerCommand(CustomerId customerId) : base(customerId)
        {
        }
    }

    internal class DeleteCustomerCommandHandler : CommandHandler<CustomerAggregate, CustomerId, DeleteCustomerCommand>
    {
        private readonly IReadModelPopulator _readModelPopulator;

        public DeleteCustomerCommandHandler(IReadModelPopulator readModelPopulator)
        {
            _readModelPopulator = readModelPopulator;
        }

        public override Task ExecuteAsync(CustomerAggregate aggregate, DeleteCustomerCommand command,
            CancellationToken cancellationToken)
        {
            _readModelPopulator.DeleteAsync(command.AggregateId.Value, typeof(CustomerReadModel), cancellationToken);

            aggregate.DeleteCustomer();

            return Task.CompletedTask;
        }
    }
}