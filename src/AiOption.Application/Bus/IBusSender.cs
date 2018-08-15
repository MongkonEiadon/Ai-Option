using System.Threading;
using System.Threading.Tasks;

namespace AiOption.Application.Bus
{
    public interface IBusSender<TQueue, in TMessage> {
        Task SendAsync(TMessage item, CancellationToken ctx = default(CancellationToken));
    }

}
