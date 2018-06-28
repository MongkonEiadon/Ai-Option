using System.Threading;
using System.Threading.Tasks;
using iqoption.domain.Common.Configuration;
using iqoption.domain.Orders;
using iqoption.servicebus.Abstracts;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;

namespace iqoption.servicebus.Position {
    public interface IOrderReceiver {
    }

    public class OrderReceiver : QueueReceiver<Order>, IOrderReceiver {
        public OrderReceiver(IOptions<AzureServiceBusConfiguration> config)
            : base(config.Value.ConnectionString, "order", ReceiveMode.PeekLock) {
            Configuration = config.Value;
        }

        public AzureServiceBusConfiguration Configuration { get; }

        internal override async Task MessageHandler(Message message, CancellationToken cancellationToken) {
            var msg = GetBody(message);

            await _client.CompleteAsync(message.SystemProperties.LockToken);
        }

        internal override Task ExceptionHandler(ExceptionReceivedEventArgs exceptionReceivedEvent) {
            return Task.CompletedTask;
        }
    }
}