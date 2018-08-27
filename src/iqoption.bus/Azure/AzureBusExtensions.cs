using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace iqoption.bus.Azure
{
    public static class AzureBusExtensions
    {
        public static IServiceCollection AddAzureServiceBus(this IServiceCollection services) {

            services.AddTransient(typeof(IBusSender<,>), typeof(AzureQueueSender<,>));
            services.AddTransient(typeof(IBusReceiver<,>), typeof(AzureQueueReceiver<,>));
            services.AddSingleton(c => {
                var config = c.GetService<IConfigurationRoot>();
                return new AzureBusConfiguration() {
                    ConnectionString = config?.GetConnectionString("azurebus")
                };
            });

            return services;
        }
    }
}
