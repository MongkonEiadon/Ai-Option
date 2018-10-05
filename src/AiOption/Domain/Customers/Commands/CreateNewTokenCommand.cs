using System.Threading;
using System.Threading.Tasks;
using AiOption.Query.Customers;
using EventFlow.Commands;
using EventFlow.Queries;

namespace AiOption.Domain.Customers.Commands
{
    public class CreateNewTokenCommand : Command<CustomerAggregate, CustomerId>
    {
        public CreateNewTokenCommand(CustomerId aggregateId) : base(aggregateId)
        {
        }
    }

    class CreateNewTokenCommandHandler : CommandHandler<CustomerAggregate, CustomerId, CreateNewTokenCommand>
    {
        private readonly IQueryProcessor _queryProcessor;

        public CreateNewTokenCommandHandler(IQueryProcessor queryProcessor) {
            _queryProcessor = queryProcessor;
        }

        public override async Task ExecuteAsync(CustomerAggregate aggregate, CreateNewTokenCommand command,
            CancellationToken cancellationToken) {

            var customer = await _queryProcessor.ProcessAsync(new QueryCustomerById(aggregate.Id, true), cancellationToken);



        }
    }
}