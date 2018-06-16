using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
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
            _traderManager.AppendUser("master.trader.xm6@hotmail.com", "123456789xyz");


           var interval =  Observable
                .Interval(TimeSpan.FromMinutes(1), Scheduler.Immediate)
                .Publish();

            interval
                .Select(x => _followerManager.GetActiveAccountNotOnFollowersTask().Result)
                .Subscribe(x => {
                    x.ForEach(y => {
                        _followerManager.AppendUser(y.IqOptionUserName, y.Password, _traderManager.TradersInfoDataObservable());
                    });
                });

            interval
                .Select(x => _followerManager.GetInActiveAccountOnFollowersTask().Result)
                .Subscribe(x => {
                    x.ForEach(y => {
                        _followerManager.RemoveByEmailAddress(y.IqOptionUserName);
                    });
                });

            interval.Connect();

            
            

            return Task.CompletedTask;
        }

    }
}