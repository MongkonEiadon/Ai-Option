using System;
using System.Threading;
using System.Threading.Tasks;

namespace iqoption.bus
{
    public interface IBusSender<TQueue, in TMessage> {
        Task SendAsync(TMessage item, CancellationToken ctx = default(CancellationToken));
    }

    public interface IBusReceiver<TQueue, out TMessage> {
        IObservable<TMessage> MessageObservable { get; }
    }

}
