using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Application.Bus;

using EventFlow.Core;

using Microsoft.Azure.ServiceBus;

using Newtonsoft.Json;

namespace AiOption.Infrastructure.Bus.Azure
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

            var json = JsonConvert.SerializeObject(item);
            var bytes = Encoding.UTF8.GetBytes(json);
            return QueueClient.SendAsync(new Message(bytes));
        }
    }
}
