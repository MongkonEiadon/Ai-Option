using System;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using iqoption.core.Collections;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Command;
using iqoption.domain.IqOption.Commands;
using iqoptionapi.models;
using Microsoft.Extensions.Logging;

namespace iqoption.trading.services {
    public interface IMasterTraderManager {
        ConcurrencyReactiveCollection<IqOptionApiClient> Traders { get; }
        IObservable<InfoData> MasterTradersInfoDataStream();
        Task AppendUserAsync(string email, string password);
    }

    public class MasterTradersManager : IMasterTraderManager {
        private readonly ILogger _logger;
        private readonly ICommandBus _commandBus;

        private Subject<InfoData> _infoDataSubject = new Subject<InfoData>();

        public IqOptionApiClient Trader;

        public MasterTradersManager(ILogger logger,
            ICommandBus commandBus) {
            _logger = logger;
            _commandBus = commandBus;
        }

        public ConcurrencyReactiveCollection<IqOptionApiClient> Traders { get; }

        public IObservable<InfoData> MasterTradersInfoDataStream() {
            return _infoDataSubject;
        }

        public async Task AppendUserAsync(string email, string password) {
            Trader?.Dispose();


            var result = _commandBus.PublishAsync(new IqLoginCommand(IqOptionIdentity.New, email, password), default(CancellationToken));
            if (result.IsCompletedSuccessfully) {
                Trader = new IqOptionApiClient(email, password);
                if (Trader.ApiClient.ConnectAsync().Result) {
                    Trader.ApiClient.InfoDatasObservable
                        .Subscribe(x => {
                            if (x?.Any() ?? false) {
                                _infoDataSubject.OnNext(x[0]);
                            }
                        });
                }
            }
        }
    }
}