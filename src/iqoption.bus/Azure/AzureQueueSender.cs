using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using iqoption.core.Extensions;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;

namespace iqoption.bus.Azure
{
    /// <inheritdoc />
    public class AzureQueueSender<TQueue, TMessage> : IBusSender<TQueue, TMessage> {
        private readonly AzureBusConfiguration _configuration;


        private IQueueClient QueueClient { get; }

        public AzureQueueSender(AzureBusConfiguration configuration) {
            _configuration = configuration;

            QueueClient = new QueueClient(_configuration.ConnectionString, typeof(TQueue).Name, ReceiveMode.ReceiveAndDelete);
        }
        
     

        public Task SendAsync(TMessage item, CancellationToken ctx = default(CancellationToken)) {

            var json = item.AsJson();
            var bytes = Encoding.UTF8.GetBytes(json);
            return QueueClient.SendAsync(new Message(bytes));
        }
    }
}
