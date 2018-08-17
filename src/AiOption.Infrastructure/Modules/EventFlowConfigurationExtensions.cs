using Autofac;

using AutoMapper.Configuration;

using EventFlow.Autofac.Extensions;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.MsSql;
using EventFlow.MsSql.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AiOption.Infrastructure.Modules {

    public static class EventFlowConfigurationExtensions {

        public static IServiceCollection AddEventFlowModule(this IServiceCollection This, ContainerBuilder builer, IConfigurationRoot configuration) {


            This.AddEventFlow(config => {
                EventFlowOptionsAutofacExtensions.UseAutofacContainerBuilder(config, builer)
                    .Configure(c => c.IsAsynchronousSubscribersEnabled = true)
                    .ConfigureMsSql(MsSqlConfiguration.New.SetConnectionString(configuration.GetConnectionString("aioptiondb")));
            });

            return This;
        }


    }

}