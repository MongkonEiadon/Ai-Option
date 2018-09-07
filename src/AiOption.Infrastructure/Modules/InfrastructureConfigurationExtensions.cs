using AiOption.Application;
using AiOption.Domain;
using AiOption.Domain.Common;
using AiOption.Domain.IqAccounts.ReadModels;
using AiOption.Infrastructure.DataAccess;

using Autofac;

using AutoMapper;

using EventFlow.Autofac.Extensions;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Extensions;
using EventFlow.MsSql;
using EventFlow.MsSql.Extensions;
using EventFlow.Snapshots.Strategies;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AiOption.Infrastructure.Modules {

    public static class InfrastructureConfigurationExtensions {

        public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services) {

            //
            services.AddAutoMapper();
            services.AddIdentity<CustomerDto, CustomerLevelDto>(identity => {
                identity.Password.RequireDigit = true;
                identity.Password.RequireLowercase = false;
                identity.Password.RequireNonAlphanumeric = false;
                identity.Password.RequiredLength = 6;
                identity.Password.RequiredUniqueChars = 0;
            });

            return services;
        }

        public static IServiceCollection AddEventFlowInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            ContainerBuilder builder) {

            services.AddEventFlow(config => {
                config.UseAutofacContainerBuilder(builder)
                    .Configure(c => c.IsAsynchronousSubscribersEnabled = true)
                    .ConfigureMsSql(MsSqlConfiguration.New.SetConnectionString(configuration.GetConnectionString("aioptiondb")));

                config.UseMssqlEventStore();
                config.UseMsSqlSnapshotStore();
                config.UseMssqlReadModel<IqAccountReadModel>();
                config.AddDefaults(typeof(BaseResult).Assembly);
                config.AddDefaults(AiAssembly.ApplicationAssembly);
                config.RegisterServices(c => { c.Register(ct => SnapshotEveryFewVersionsStrategy.With(100)); });
            });

            return services;

        }

    }

}