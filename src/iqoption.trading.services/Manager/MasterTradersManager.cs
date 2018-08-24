using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Command;
using iqoption.domain.IqOption.Commands;
using IqOptionApi.Models;
using IqOptionApi.ws;
using Microsoft.Extensions.Logging;

namespace iqoption.trading.services.Manager {
    public interface IMasterTraderManager {
        IObservable<InfoData> MasterOpenOrderStream { get; }
        Task AppendUserAsync(string email, string password);

        IqOptionWebSocketClient MasterClient { get; }
    }

    public class MasterTradersManager : IMasterTraderManager {
        private readonly ICommandBus _commandBus;
        private readonly ILogger _logger;

        private static readonly Subject<InfoData> _infoDataSubject = new Subject<InfoData>();

        public IObservable<InfoData> MasterOpenOrderStream { get; private set; }

        public IqOptionApiClient Trader;

        public MasterTradersManager(ILogger logger,
            ICommandBus commandBus) {
            _logger = logger;
            _commandBus = commandBus;

            MasterOpenOrderStream = _infoDataSubject.Publish().RefCount();
        }
        
        

        public async Task AppendUserAsync(string email, string password) {
            Trader?.Dispose();

            var result = await _commandBus.PublishAsync(new IqLoginCommand(IqIdentity.New, email, password), default(CancellationToken));

            if (result.IsSuccess) {

                _logger.LogInformation($"Traders Loged in with email {email}, succeed!");

                await _commandBus.PublishAsync(new StoreSsidCommand(IqIdentity.New, email, result.Ssid), default(CancellationToken));

                MasterClient = new IqOptionWebSocketClient(ws => {
                    ws.OpenSecuredSocketAsync(result.Ssid);
                    ws.InfoDataObservable.Subscribe(x => {
                        if ((x?.Any() ?? false) && x[0].Win == "equal") {
                            _infoDataSubject.OnNext(x[0]);
                        }
                    });
                });
            }

        }

        public IqOptionWebSocketClient MasterClient { get; private set; }
    }
}