using System.Collections.Generic;
using System.IO;
using iqoption.domain.Common.Configuration;
using iqoption.servicebus.Abstracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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