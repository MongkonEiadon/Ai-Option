using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using iqoption.domain.Common;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;

namespace iqoption.servicebus.Abstracts
{
    public abstract class QueueReceiver<T>
        where T : BaseEntity
    {
        protected readonly IMessageReceiver _client;

        public T GetBody(Message message) {
            var json = Encoding.UTF8.GetString(message.Body);
            var msg = JsonConvert.DeserializeObject<T>(json);

            return msg;
        }

        public QueueReceiver(string url, string queueName, ReceiveMode receiveMode)
        {
            _client = new MessageReceiver(url, queueName, receiveMode: receiveMode);
            _client.RegisterMessageHandler(MessageHandler, new MessageHandlerOptions(ExceptionHandler)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1
            });
        }

        internal abstract Task MessageHandler(Message message, CancellationToken cancellationToken);
        internal abstract Task ExceptionHandler(ExceptionReceivedEventArgs exceptionReceivedEvent);
    }
}
