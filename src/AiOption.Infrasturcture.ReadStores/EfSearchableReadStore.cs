using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Query;
using EventFlow.Core;
using EventFlow.Core.RetryStrategies;
using EventFlow.EntityFramework;
using EventFlow.EntityFramework.ReadStores;
using EventFlow.Extensions;
using EventFlow.Logs;
using EventFlow.ReadStores;
using Microsoft.EntityFrameworkCore;

namespace AiOption.Infrasturcture.ReadStores
{
    public class EfSearchableReadStore<TReadModel, TDbContext> : 
        EntityFrameworkReadModelStore<TReadModel, TDbContext>,
        ISearchableReadModelStore<TReadModel>
        where TReadModel : class, IReadModel, new() where TDbContext : DbContext
    {
        private readonly IDbContextProvider<TDbContext> _contextProvider;


        public EfSearchableReadStore(
            IBulkOperationConfiguration bulkOperationConfiguration, 
            ILog log, 
            IReadModelFactory<TReadModel> readModelFactory, 
            IDbContextProvider<TDbContext> contextProvider, 
            ITransientFaultHandler<IOptimisticConcurrencyRetryStrategy> transientFaultHandler) 
                : base(bulkOperationConfiguration, log, readModelFactory, contextProvider, transientFaultHandler)
        {
            _contextProvider = contextProvider;
        }

        public async Task<IReadOnlyCollection<TReadModel>> FindAsync(Predicate<TReadModel> predicate,
            CancellationToken cancellationToken)
        {
            using (var dbContext = _contextProvider.CreateContext())
            {

                var readModelType = typeof(TReadModel);

                var entity = await dbContext.Set<TReadModel>()
                    .Where(x => predicate(x))
                    .ToListAsync(cancellationToken: cancellationToken);

                if (!entity.Any())
                {
                    Log.Verbose(() => $"Could not find any Entity Framework read model '{readModelType.PrettyPrint()}'");
                    return Enumerable.Empty<TReadModel>().ToList();
                }

                //var entry = dbContext.Entry(entity);
                //var version = descriptor.GetVersion(entry);

                Log.Verbose(() =>
                    $"Found Entity Framework read model '{readModelType.PrettyPrint()}'");

                //return version.HasValue
                //    ? ReadModelEnvelope<TReadModel>.With(id, entity, version.Value)
                //    : ReadModelEnvelope<TReadModel>.With(id, entity);

                return entity;
            }
        }
        
    }
}