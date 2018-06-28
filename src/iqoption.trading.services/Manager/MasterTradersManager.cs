using System;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using iqopoption.core;
using iqoption.core.Collections;
using iqoption.domain.IqOption.Command;
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
        private readonly ISession _session;

        private Subject<InfoData> _infoDataSubject = new Subject<InfoData>();

        public IqOptionApiClient Trader;

        public MasterTradersManager(ILogger logger,
            ISession session) {
            _logger = logger;
            _session = session;
        }

        public ConcurrencyReactiveCollection<IqOptionApiClient> Traders { get; }

        public IObservable<InfoData> MasterTradersInfoDataStream() {
            return _infoDataSubject;
        }

        public async Task AppendUserAsync(string email, string password) {
            Trader?.Dispose();

            var result = await _session.Send(new LoginCommand(email, password));
            if (result.IsSuccess) {
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