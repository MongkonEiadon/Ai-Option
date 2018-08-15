using AiOption.Application.Bus;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AiOption.Infrastructure.Bus.Azure {

    public static class AzureBusConfigurationExtensions {

        public static IServiceCollection AddAzureServiceBus(this IServiceCollection services) {

            services.AddTransient(typeof(IBusSender<,>), typeof(AzureQueueSender<,>));
            services.AddTransient(typeof(IBusReceiver<,>), typeof(AzureQueueReceiver<,>));
            services.AddSingleton(c => {
                var config = c.GetService<IConfigurationRoot>();
                return new AzureBusConfiguration() {
                    ConnectionString = config?.GetSection(typeof(AzureBusConfiguration).Name)?["ConnectionString"]
                };
            });

            return services;
        }

    }

}
