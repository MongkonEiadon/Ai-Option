using iqoption.servicebus.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace iqoption.servicebus.Position {
    public static class ServiceBusConfiguration {
        public static IServiceCollection AddIqOptionMessageBusService(
            this IServiceCollection This) {
            This.AddSingleton<OrderReceiver>();
            This.AddTransient<IOrderSender, OrderSender>();
            This.AddTransient(typeof(QueueSender<>));

            return This;
        }
    }
}