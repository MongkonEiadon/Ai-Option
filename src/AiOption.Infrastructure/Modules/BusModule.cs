using AiOption.Application.Bus;
using AiOption.Infrastructure.Bus.Azure;

using Autofac;

using Microsoft.Extensions.Configuration;

namespace AiOption.Infrastructure.Modules {

    public class BusModule : Module {

        protected override void Load(ContainerBuilder builder) {

            builder.Register(c => {
                var configuration = c.Resolve<IConfigurationRoot>();
                var connstring = configuration.GetConnectionString("azurebus");

                return new AzureBusConfiguration {
                    ConnectionString = connstring
                };
            });

            builder.RegisterGeneric(typeof(AzureQueueReceiver<,>)).As(typeof(IBusReceiver<,>))
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(AzureQueueSender<,>)).As(typeof(IBusSender<,>)).InstancePerLifetimeScope();

        }

    }

}