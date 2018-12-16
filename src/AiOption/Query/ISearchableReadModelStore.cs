using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Logs;
using EventFlow.ReadStores;
using EventFlow.ReadStores.InMemory;

namespace AiOption.Query
{
    public interface ISearchableReadModelStore<TReadModel> : IReadModelStore<TReadModel>
        where TReadModel : class, IReadModel, new()
    {
        Task<IReadOnlyCollection<TReadModel>> FindAsync(
            Predicate<TReadModel> predicate,
            CancellationToken cancellationToken);

        Task<bool> AnyAsync(
            Predicate<TReadModel> predicate,
            CancellationToken cancellationToken);
    }

    public class InMemorySearchableReadStore<TReadModel> : 
        InMemoryReadStore<TReadModel>,
        ISearchableReadModelStore<TReadModel>
        where TReadModel : class, IReadModel, new()
    {
        private readonly IInMemoryReadStore<TReadModel> _readStore;

        public InMemorySearchableReadStore(IInMemoryReadStore<TReadModel> readStore,
            ILog log)
            : base(log)
        {
            _readStore = readStore;
        }

        public new Task<IReadOnlyCollection<TReadModel>> FindAsync(
            Predicate<TReadModel> predicate,
            CancellationToken cancellationToken)
        {
            return _readStore.FindAsync(predicate, cancellationToken);
        }

        public Task<bool> AnyAsync(Predicate<TReadModel> predicate, CancellationToken cancellationToken)
        {
            return _readStore.FindAsync(predicate, cancellationToken)
                .ContinueWith(t => t.Result.Any(), cancellationToken);
        }
    }
}