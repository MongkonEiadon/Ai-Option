using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using iqoptionapi.models;
using Microsoft.Extensions.Logging;

namespace iqoption.trading.services
{

    public interface ITradingPersistenceService
    {

        bool IsStarted { get; }
        Task InitializeTradingsServiceAsync();


    }
    public class TradingPersistenceService : ITradingPersistenceService
    {

        private readonly ITraderManager _traderManager;
        private readonly IFollowerManager _followerManager;
        private readonly ILogger _logger;
        public bool IsStarted { get; private set; }
        public TradingPersistenceService(
            ITraderManager traderManager,
            IFollowerManager followerManager,
            ILogger<TradingPersistenceService> logger)
        {
            _traderManager = traderManager;
            _followerManager = followerManager;
            _logger = logger;



            //merge when addding new client
            traderManager.Traders.AddObservable()
                .Merge(followerManager.Followers.AddObservable())
                .Select(x => x.ApiClient)
                .Subscribe(x => x.ConnectAsync());
        }


        public Task InitializeTradingsServiceAsync()
        {

            IsStarted = true;
            
            //_traderManager.AppendUser("mongkon.eiadon@gmail.com", "Code11054");

            _traderManager.AppendUser("master.trader.xm6@hotmail.com", "123456789xyz");
            _followerManager.AppendUser("master.trader.xm3@hotmail.com", "123456789def", _traderManager.TradersInfoDataObservable());
            _followerManager.AppendUser("master.trader.xm5@hotmail.com", "123456789abc", _traderManager.TradersInfoDataObservable());
            _followerManager.AppendUser("liie.m@excelbangkok.com", "Code11054", _traderManager.TradersInfoDataObservable());




            return Task.CompletedTask;
        }

    }
}