using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using iqoption.domain.Common.Configuration;
using iqoption.domain.Orders;
using iqoption.servicebus.Abstracts;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;

namespace iqoption.servicebus.Position {


    public interface IOrderReceiver
    {

    }
    public class OrderReceiver : QueueReceiver<Order>, IOrderReceiver {
     
        public AzureServiceBusConfiguration Configuration { get; }

        public OrderReceiver(IOptions<AzureServiceBusConfiguration> config)
            : base(config.Value.ConnectionString, "order", ReceiveMode.PeekLock) {
            this.Configuration = config.Value;
        }

        internal override async Task MessageHandler(Message message, CancellationToken cancellationToken) {
            var msg = base.GetBody(message);

            await _client.CompleteAsync(message.SystemProperties.LockToken);

        }

        internal override Task ExceptionHandler(ExceptionReceivedEventArgs exceptionReceivedEvent) {


            return Task.CompletedTask;
        }
    }
}