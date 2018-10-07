using System;
using System.Data;
using System.Data.SqlClient;
using AiOption.Infrastructure.ReadStores.ReadModels;
using AiOption.Infrasturcture.ReadStores.ReadModels;
using EventFlow;
using EventFlow.EntityFramework;
using EventFlow.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

namespace AiOption.Infrasturcture.ReadStores
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
                .UseEntityFrameworkReadModel<CustomerReadModel, AiOptionDbContext>();
        }
    }
}