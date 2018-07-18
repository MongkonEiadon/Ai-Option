using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using iqoption.trading.services.Manager;
using Microsoft.Extensions.Logging;

namespace iqoption.trading.services {
    public interface ITradingPersistenceService {
        bool IsStarted { get; }
        Task InitializeTradingsServiceAsync();
    }

    public class TradingPersistenceService : ITradingPersistenceService {
        private readonly IFollowerManager _followerManager;
        private readonly ILogger _logger;

        private readonly IMasterTraderManager _masterTraderManager;

        public TradingPersistenceService(
            IMasterTraderManager masterTraderManager,
            IFollowerManager followerManager,
            ILogger<TradingPersistenceService> logger) {
            _masterTraderManager = masterTraderManager;
            _followerManager = followerManager;
            _logger = logger;

            //merge when addding new client
            //masterTraderManager.Traders.AddObservable().Merge(
            followerManager.Followers.AddObservable()
                .Select(x => x.ApiClient)
                .Subscribe(x => x.ConnectAsync());
        }

        public bool IsStarted { get; private set; }


        public async Task InitializeTradingsServiceAsync() {
            IsStarted = true;
            await _masterTraderManager.AppendUserAsync("Tlezx10-rr@hotmail.com", "natthon2547");


            var result = await _followerManager.GetActiveAccountNotOnFollowersTask();


            result.ForEach(y => _followerManager.AppendUser(y.IqOptionUserName, y.Password));

            //var interval = Observable
            //    .Interval(TimeSpan.FromSeconds(60), Scheduler.Immediate)
            //    .Publish();

            //interval
            //    .Select(x => _followerManager.GetActiveAccountNotOnFollowersTask().Result)
            //    .Subscribe(x => {
            //        x.ForEach(y => {
            //            _followerManager.AppendUser(y.IqOptionUserName, y.Password);
            //        });
            //    });

            //interval
            //    .Select(x => _followerManager.GetInActiveAccountNotOnFollowersTask().Result)
            //    .Subscribe(x => { x.ForEach(y => { _followerManager.RemoveByEmailAddress(y.IqOptionUserName); }); });

            //interval.Connect();

        
            foreach (var client in _followerManager.Followers) {
                client.SubScribeForTraderStream(_masterTraderManager.MasterOpenOrderStream);
            }
            
        }
    }

    public class MasterTradeAccount {

    }
}