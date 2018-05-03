using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using iqoptionapi.models;
using Microsoft.Extensions.Logging;

namespace iqoption.trading.services
{
    public interface ITraderManager {

        IObservable<InfoData[]> TradersInfoData { get; }

    }
    public  class TradersManager : ITraderManager {
        private readonly ILogger _logger;
        public IObservable<InfoData[]> TradersInfoData { get; }

        public TradersManager(ILogger logger) {
            _logger = logger;


            TradersInfoData = Observable.Interval(TimeSpan.FromSeconds(5))
                .Select(x => new[] {new InfoData(){ Id = DateTime.Now.Ticks },});
        }
    }



}
