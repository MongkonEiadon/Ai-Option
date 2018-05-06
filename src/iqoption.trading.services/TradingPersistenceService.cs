using System;
using System.Threading.Tasks;
using iqoptionapi;
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
        }


        public async Task InitializeTradingsServiceAsync() {

            IsStarted = true;

            var client = new IqOptionApi("mongkon.eiadon@gmail.com", "Code11054");
            await client.ConnectAsync();
            
        }
    }
}