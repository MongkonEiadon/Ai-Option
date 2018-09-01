using System;

namespace AiOption.Application.Bus {

    public interface IBusReceiver<TQueue, out TMessage> {

        IObservable<TMessage> MessageObservable { get; }

    }

}