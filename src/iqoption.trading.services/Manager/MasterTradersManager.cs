using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using iqoption.core.Collections;
using iqoptionapi.models;
using Microsoft.Extensions.Logging;

namespace iqoption.trading.services
{
    public interface IMasterTraderManager
    {
        ConcurrencyReactiveCollection<IqOptionApiClient> Traders { get; }
        IObservable<InfoData> MasterTradersInfoDataStream();

        void AppendUser(string email, string password);

    }
    public class MasterTradersManager : IMasterTraderManager
    {
        private readonly ILogger _logger;
        public ConcurrencyReactiveCollection<IqOptionApiClient> Traders { get; }

        private Subject<InfoData> _infoDataSubject = new Subject<InfoData>();

        public IObservable<InfoData> MasterTradersInfoDataStream() { return _infoDataSubject; }

        public IqOptionApiClient Trader;

        public MasterTradersManager(ILogger logger)
        {
            _logger = logger;
        }

        public void AppendUser(string email, string password)
        {
            Trader?.Dispose();
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