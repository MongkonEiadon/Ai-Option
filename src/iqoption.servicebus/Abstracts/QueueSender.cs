using System.Text;
using System.Threading.Tasks;
using iqoption.domain.Common;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;

namespace iqoption.servicebus.Abstracts {
    public class QueueSender<T> where T: BaseEntity
    {
        internal readonly IMessageSender _client;

        public QueueSender(string url, string queueName)
        {
            _client = new MessageSender(url, queueName);

            var a = new QueueClient(url, "order");
        }

        public async Task SendMessage(T task) 
        {
            var serialized = JsonConvert.SerializeObject(task);
            var body = Encoding.UTF8.GetBytes(serialized);

            var message = new Message(body) {
                MessageId = task.Id,
                Body = body
            };

            await _client.SendAsync(message);
        }
    }
}