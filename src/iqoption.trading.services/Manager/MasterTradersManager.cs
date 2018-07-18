using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using iqoption.core.Collections;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Commands;
using iqoptionapi.models;
using iqoptionapi.ws;
using Microsoft.Extensions.Logging;

namespace iqoption.trading.services {
    public interface IMasterTraderManager {
        IObservable<InfoData> MasterOpenOrderStream { get; }
        Task AppendUserAsync(string email, string password);

        IqOptionWebSocketClient MasterClient { get; }
    }

    public class MasterTradersManager : IMasterTraderManager {
        private readonly ICommandBus _commandBus;
        private readonly ILogger _logger;

        private static Subject<InfoData> _infoDataSubject = new Subject<InfoData>();

        public IObservable<InfoData> MasterOpenOrderStream { get; private set; }

        public IqOptionApiClient Trader;

        public MasterTradersManager(ILogger logger,
            ICommandBus commandBus) {
            _logger = logger;
            _commandBus = commandBus;

            MasterOpenOrderStream = _infoDataSubject.Publish().RefCount();
        }
        
        

        public Task AppendUserAsync(string email, string password) {
            Trader?.Dispose();

            return _commandBus.PublishAsync(new IqLoginCommand(IqOptionIdentity.New, email, password), default(CancellationToken))
                .ContinueWith(t => {
                    if (t.Result.IsSuccess) {
                        MasterClient = new IqOptionWebSocketClient(t.Result.Ssid, ws => {
                            ws.InfoDataObservable.Subscribe(x => {
                                if ((x?.Any() ?? false) && x[0].Win == "equal") {
                                    _infoDataSubject.OnNext(x[0]);
                                }
                            });
                        
                        });
                    }
                });
        }

        public IqOptionWebSocketClient MasterClient { get; private set; }
    }
}