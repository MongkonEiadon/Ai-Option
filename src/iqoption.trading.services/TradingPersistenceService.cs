using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using iqoptionapi.models;
using Microsoft.Extensions.Logging;

namespace iqoption.trading.services {

    public interface ITradingPersistenceService {

        bool IsStarted { get; }
        Task InitializeTradingsServiceAsync();


    }
    public class TradingPersistenceService : ITradingPersistenceService {
        
        private readonly ITraderManager _traderManager;
        private readonly IFollowerManager _followerManager;
        private readonly ILogger _logger;
        public bool IsStarted { get; private set; }
        public TradingPersistenceService(
            ITraderManager traderManager, 
            IFollowerManager followerManager,
            ILogger<TradingPersistenceService> logger) {
            _traderManager = traderManager;
            _followerManager = followerManager;
            _logger = logger;

            

            //merge when addding new client
            traderManager.Traders.AddObservable()
                .Merge(followerManager.Followers.AddObservable())
                .Select(x => x.ApiClient)
                .Subscribe(x => x.ConnectAsync());
        }


        public Task InitializeTradingsServiceAsync() {

            IsStarted = true;

            _traderManager.AppendUser("mongkon.eiadon@gmail.com", "Code11054");

            _followerManager.AppendUser("liie.m@excelbangkok.com", "Code11054", _traderManager.TradersInfoDataObservable());
            

            return Task.CompletedTask;
        }
        
    }
}