using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using AiOption.Domain.Customers.Events;
using AiOption.Domain.IqAccounts.Commands;
using AiOption.Domain.Sagas.Terminate.Events;
using AiOption.Query.IqAccounts;
using EventFlow.Aggregates;
using EventFlow.Queries;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;

namespace AiOption.Domain.Sagas.Terminate
{
    public class TerminateSaga : 
        AggregateSaga<TerminateSaga, TerminateSagaId, TerminateSagaLocator>,
        ISagaIsStartedBy<CustomerAggregate, CustomerId, TerminateRequested>, // stop this until double execute saga
        ISagaHandles<CustomerAggregate, CustomerId, TerminateCustomerCompleted>
        
    {
        private readonly IQueryProcessor _queryProcessor;

        private List<string> _state = new List<string>();
        public IReadOnlyCollection<string> State => _state;

        public TerminateSaga(TerminateSagaId id, IQueryProcessor queryProcessor) : base(id)
        {
            _queryProcessor = queryProcessor;

            Register<TerminateStarted>(e => _state.Add(e.GetType().Name));
            Register<TerminateCompleted>(e =>
            {
                _state.Add(e.GetType().Name);
                Complete();
            });
        }
        

        public Task HandleAsync(
            IDomainEvent<CustomerAggregate, CustomerId, TerminateRequested> domainEvent, 
            ISagaContext sagaContext, 
            CancellationToken cancellationToken)
        {
            this.Emit(new TerminateStarted());

            return _queryProcessor.ProcessAsync(new QueryIqAccountsByCustomerId(domainEvent.AggregateIdentity),
                cancellationToken).ContinueWith(t =>
            {
                if (t.Result.Any())
                {
                    foreach (var iqAccount in t.Result)
                    {
                        Publish(new TerminateIqAccountCommand(iqAccount.Id));
                    }
                }

                Publish(new TerminateCustomerCommand(domainEvent.AggregateIdentity));
            }, cancellationToken);
        }

        public Task HandleAsync(
            IDomainEvent<CustomerAggregate, CustomerId, TerminateCustomerCompleted> domainEvent, 
            ISagaContext sagaContext, 
            CancellationToken cancellationToken)
        {
            Emit(new TerminateCompleted());
            Complete();
            return Task.CompletedTask;
        }
    }
}
