using Autofac;

using AutoMapper;

using EventFlow.Autofac.Extensions;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.MsSql;
using EventFlow.MsSql.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AiOption.Infrastructure.Modules {

    public static class InfrastructureConfigurationExtensions {

        public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services) {

            //
            services.AddAutoMapper();

            return services;
        }

        public static IServiceCollection AddEventFlowInfrastructure(
            this IServiceCollection services, 
            IConfigurationRoot configuration,
            ContainerBuilder builder) {

            

            services.AddEventFlow(config => {
                config.UseAutofacContainerBuilder(builder)
                    .Configure(c => c.IsAsynchronousSubscribersEnabled = true)
                    .ConfigureMsSql(MsSqlConfiguration.New.SetConnectionString(configuration.GetConnectionString("aioptiondb")));
            });

            return services;

        }

    }

}