using System;
using System.Threading;
using System.Threading.Tasks;

using EventFlow.Aggregates;
using EventFlow.Sagas;

namespace AiOption.Domain.Common.Saga {

    public abstract class BaseIdSagaLocator : ISagaLocator {

        private readonly Func<string, ISagaId> _sagaIdGenerator;

        protected BaseIdSagaLocator(string metadataKey, Func<string, ISagaId> sagaIdGenerator) {
            _sagaIdGenerator = sagaIdGenerator;
            MetadataKey = metadataKey;
        }

        protected string MetadataKey { get; }

        public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken) {

            var id = domainEvent.Metadata[MetadataKey];

            return Task.FromResult(_sagaIdGenerator(id));
        }

    }

}