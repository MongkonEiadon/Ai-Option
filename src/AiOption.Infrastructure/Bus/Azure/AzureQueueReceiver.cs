using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Application.Bus;

using Microsoft.Azure.ServiceBus;

using Newtonsoft.Json;

namespace AiOption.Infrastructure.Bus.Azure {

    public class AzureQueueReceiver<TQueue, TMessage> : IBusReceiver<TQueue, TMessage> {

        private readonly AzureBusConfiguration _configuration;


        private readonly Subject<TMessage> _message = new Subject<TMessage>();


        public AzureQueueReceiver(AzureBusConfiguration configuration) {
            _configuration = configuration;

            QueueClient = new QueueClient(_configuration.ConnectionString, typeof(TQueue).Name,
                ReceiveMode.ReceiveAndDelete);
            QueueClient.RegisterMessageHandler(Handler, ExceptionReceivedHandler);
        }

        private IQueueClient QueueClient { get; }
        public IObservable<TMessage> MessageObservable => _message.Publish().RefCount();

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg) {
            _message.OnError(arg.Exception);

            return Task.CompletedTask;
        }

        private Task Handler(Message arg1, CancellationToken arg2) {

            var json = Encoding.UTF8.GetString(arg1.Body);
            var message = JsonConvert.DeserializeObject<TMessage>(json);

            if (message != null) _message.OnNext(message);

            return Task.CompletedTask;
        }

    }

}