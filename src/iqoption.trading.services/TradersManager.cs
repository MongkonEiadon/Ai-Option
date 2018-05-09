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
    public interface ITraderManager
    {
        ConcurrencyReactiveCollection<IqOptionApiClient> Traders { get; }
        IObservable<InfoData> TradersInfoDataObservable();

        void AppendUser(string email, string password);

    }
    public class TradersManager : ITraderManager
    {
        private readonly ILogger _logger;
        public ConcurrencyReactiveCollection<IqOptionApiClient> Traders { get; }

        private Subject<InfoData> _infoDataSubject = new Subject<InfoData>();

        public IObservable<InfoData> TradersInfoDataObservable() { return _infoDataSubject; }

        public TradersManager(ILogger logger)
        {
            _logger = logger;

            Traders = new ConcurrencyReactiveCollection<IqOptionApiClient>();
        }

        public void AppendUser(string email, string password)
        {

            if (Traders.All(x => x.User.Email == email))
            {
                var client = new IqOptionApiClient(email, password);
                client.ApiClient
                    .IsConnectedObservable
                    .Where(x => x)
                    .Select(x => client.ApiClient.InfoDatasObservable)
                    .Subscribe(x => x.Subscribe(informant => {
                        if (informant?.Any() ?? false)
                            _infoDataSubject.OnNext(informant[0]);
                    }));


                Traders.Add(client);

            }
        }
    }
}