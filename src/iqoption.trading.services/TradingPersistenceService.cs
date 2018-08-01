using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using iqoption.domain;
using iqoption.trading.services.Manager;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace iqoption.trading.services {
    public interface ITradingPersistenceService {
        bool IsStarted { get; }
        Task InitializeTradingsServiceAsync();

        void GetListOfSubscribe();
    }

    public class TradingPersistenceService : ITradingPersistenceService {
        private readonly IFollowerManager _followerManager;
        private readonly ILogger _logger;

        private readonly IMasterTraderManager _masterTraderManager;
        private readonly TraderAccountConfiguration _traderAccount;

        public TradingPersistenceService(
            IMasterTraderManager masterTraderManager,
            IOptions<TraderAccountConfiguration> traderAccount,
            IFollowerManager followerManager,
            ILogger<TradingPersistenceService> logger) {
            _masterTraderManager = masterTraderManager;
            _traderAccount = traderAccount.Value;
            _followerManager = followerManager;
            _logger = logger;
            
        }

        public bool IsStarted { get; private set; }


        public async Task InitializeTradingsServiceAsync() {
            IsStarted = true;
            await _masterTraderManager.AppendUserAsync(_traderAccount.EmailAddress, _traderAccount.Password);


            var result = await _followerManager.GetActiveAccountNotOnFollowersTask();


            result.ForEach(y => _followerManager.AppendUser(y, _masterTraderManager.MasterOpenOrderStream));



            var interval = Observable.Interval(TimeSpan.FromSeconds(60), Scheduler.Immediate)
                .Publish().RefCount();

            

            ////var interval = Observable
            ////    .Interval(TimeSpan.FromSeconds(60), Scheduler.Immediate)
            ////    .Publish();

            ////interval
            ////    .Select(x => _followerManager.GetActiveAccountNotOnFollowersTask().Result)
            ////    .Subscribe(x => {
            ////        x.ForEach(y => {
            ////            _followerManager.AppendUser(y.IqOptionUserName, y.Password);
            ////        });
            ////    });

            ////interval
            ////    .Select(x => _followerManager.GetInActiveAccountNotOnFollowersTask().Result)
            ////    .Subscribe(x => { x.ForEach(y => { _followerManager.RemoveByEmailAddress(y.IqOptionUserName); }); });

            ////interval.Connect();


            ////foreach (var client in _followerManager.Followers) {
            ////    client.SubScribeForTraderStream(_masterTraderManager.MasterOpenOrderStream);

        }

        public void GetListOfSubscribe() {
            var sb = new StringBuilder();

            sb
                .AppendLine($"==================================================")
                .AppendLine($"==================================================")
                .AppendLine($"============= No of User ({_followerManager.Followers.Count}) ======================")
                .AppendLine($"==================================================")
                .AppendLine($"==================================================");

            foreach (var c in _followerManager.Followers) {
                sb.AppendLine(c.Account.IqOptionUserName);
            }

            Console.Write(sb.ToString());
        }
    }

   
}