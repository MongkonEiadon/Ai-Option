using AiOption.Application;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Infrastructure.DataAccess;
using AiOption.Infrastructure.ReadStores;
using AiOption.Infrastructure.ReadStores.ReadModels;
using Autofac;

using AutoMapper;
using EventFlow;
using EventFlow.Autofac.Extensions;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.EntityFramework;
using EventFlow.EntityFramework.Extensions;
using EventFlow.Extensions;
using EventFlow.Snapshots.Strategies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CustomerReadModel = AiOption.Infrastructure.ReadStores.ReadModels.CustomerReadModel;

namespace AiOption.Infrastructure.Modules {

    public static class InfrastructureConfigurationExtensions {

        public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services) {

            //
            services.AddAutoMapper();

            return services;
        }

        public static IServiceCollection AddEventFlowInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            ContainerBuilder builder) {

            services.AddEventFlow(config => {
                config.UseAutofacContainerBuilder(builder)
                    .Configure(c => {
                        c.IsAsynchronousSubscribersEnabled = true;
                        c.ThrowSubscriberExceptions = true;
                    });

                config.AddEventFlowDefaultsForDomain();
                config.AddEventflowDefaultsForInfrastructure();
                config.UseEfCoreEventFlow();
                config.RegisterServices(c => { c.Register(ct => SnapshotEveryFewVersionsStrategy.With(100)); });
            });

            return services;

        }

        public static IEventFlowOptions UseEfCoreEventFlow(this IEventFlowOptions options)
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