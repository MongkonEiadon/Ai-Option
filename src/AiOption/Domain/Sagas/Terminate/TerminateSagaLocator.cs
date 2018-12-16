using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Sagas;

namespace AiOption.Domain.Sagas.Terminate
{
    public class TerminateSagaLocator : ISagaLocator
    {
        public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var aggregateId = domainEvent.GetIdentity();
            return Task.FromResult<ISagaId>(new TerminateSagaId($"saga-{aggregateId.Value}"));
        }
    }
}