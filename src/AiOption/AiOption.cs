using System.Reflection;
using AiOption.Domain.Customers;
using AiOption.Domain.IqAccounts;
using AiOption.Query;
using EventFlow;
using EventFlow.Extensions;
using EventFlow.ReadStores;
using EventFlow.ReadStores.InMemory;
using EventFlow.Snapshots.Strategies;

namespace AiOption
{
    public static class AiOption
    {
        public static Assembly AiOptionAssembly => typeof(AiOption).Assembly;

        public static IEventFlowOptions AddDomain(this IEventFlowOptions options)
        {
            return
                options.AddDefaults(AiOptionAssembly)
                    .RegisterServices(c => c.Register(ct => SnapshotEveryFewVersionsStrategy.With(10)));
        }

        public static IEventFlowOptions UsingDomainInMemoryReadStore(this IEventFlowOptions This)
        {
            return This
                .AddInMemoryReadStoreFor<CustomerReadModel>()
                .AddInMemoryReadStoreFor<IqAccountReadModel>();
        }

        private static IEventFlowOptions AddInMemoryReadStoreFor<TReadModel>(this IEventFlowOptions options)
            where TReadModel : class, IReadModel, new()
        {
            return options
                .UseInMemoryReadStoreFor<TReadModel>()
                .RegisterServices(r =>
                {
                    r.RegisterGeneric(typeof(ISearchableReadModelStore<>), typeof(InMemorySearchableReadStore<>));
                    r.Register<ISearchableReadModelStore<TReadModel>, InMemorySearchableReadStore<TReadModel>>();
                    r.Register<IReadModelStore<TReadModel>, InMemoryReadStore<TReadModel>>();
                });
        }
    }
}