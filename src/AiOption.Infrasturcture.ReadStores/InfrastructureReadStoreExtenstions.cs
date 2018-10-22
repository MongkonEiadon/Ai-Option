using System.Data;
using System.Data.SqlClient;
using AiOption.Domain.Customers;
using AiOption.Domain.IqAccounts;
using AiOption.Infrasturcture.ReadStores;
using AiOption.Query;
using EventFlow;
using EventFlow.EntityFramework;
using EventFlow.EntityFramework.Extensions;
using EventFlow.Extensions;
using EventFlow.ReadStores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

namespace AiOption.Infrastructure.ReadStores
{
    public static class InfrastructureReadStoreExtenstions
    {
        public static IServiceCollection AddEfConfigurationDomain(this IServiceCollection This,
            IConfiguration config)
        {
            var constring = config.GetConnectionString("aioptiondb");

            This
                .AddSingleton(config)
                .AddEntityFrameworkSqlServer()
                .AddScoped(c => new AiOptionDbContext())
                .AddTransient<IDbConnection>(c => new SqlConnection(constring))
                .AddDbContext<AiOptionDbContext>(c =>
                {
                    c.UseLoggerFactory(new NullLoggerFactory());
                    c.UseSqlServer(constring);
                });

            return This;
        }


        public static IEventFlowOptions AddInfrastructureReadStores(this IEventFlowOptions options)
        {
            options
                .ConfigureEntityFramework(EntityFrameworkConfiguration.New)
                .AddDbContextProvider<AiOptionDbContext, MsSqlDbContextProvider>()
                .AddDefaults(typeof(InfrastructureReadStoreExtenstions).Assembly)
                .ConfigureAiOptionEventStore()
                .ConfigureAiOptionSnapshotStore()
                .ConfigureInfrastructureReadModelStore();

            return options;
        }


        public static IEventFlowOptions ConfigureAiOptionEventStore(this IEventFlowOptions options)
        {
            return options
                .UseEntityFrameworkEventStore<AiOptionDbContext>();
        }


        public static IEventFlowOptions ConfigureAiOptionSnapshotStore(this IEventFlowOptions options)
        {
            return options
                .UseEntityFrameworkSnapshotStore<AiOptionDbContext>();
        }


        public static IEventFlowOptions ConfigureInfrastructureReadModelStore(this IEventFlowOptions options)
        {
            return options
                .ConfigureSearchableReadModelStore<CustomerReadModel, AiOptionDbContext>()
                .ConfigureSearchableReadModelStore<IqAccountReadModel, AiOptionDbContext>();
        }

        public static IEventFlowOptions ConfigureSearchableReadModelStore<TReadModel, TDbContext>(
            this IEventFlowOptions options)
            where TReadModel : class, IReadModel, new()
            where TDbContext : DbContext
        {
            return options
                .UseEntityFrameworkReadModel<TReadModel, TDbContext>()
                .RegisterServices(r =>
                {
                    r.RegisterGeneric(typeof(ISearchableReadModelStore<>), typeof(EfSearchableReadStore<,>));
                    r.Register<ISearchableReadModelStore<TReadModel>, EfSearchableReadStore<TReadModel, TDbContext>>();
                });
        }
    }
}