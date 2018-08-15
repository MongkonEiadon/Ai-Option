using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.IqOptionAccount;
using AiOption.Infrastructure.Bus;

using EventFlow.Queries;
using iqoption.domain;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Queries;
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
        private readonly IQueryProcessor _queryProcessor;
        private readonly ILogger _logger;

        private readonly IMasterTraderManager _masterTraderManager;
        private readonly IBusReceiver<ActiveAccountQueue, SetActiveAccountStatusItem> _activeAccountBusReceiver;
        private readonly TraderAccountConfiguration _traderAccount;

        public TradingPersistenceService(
            IMasterTraderManager masterTraderManager,
            IOptions<TraderAccountConfiguration> traderAccount,
            IBusReceiver<ActiveAccountQueue, SetActiveAccountStatusItem> activeAccountBusReceiver,
            IFollowerManager followerManager,
            EventFlow.Queries.IQueryProcessor queryProcessor,
            ILogger<TradingPersistenceService> logger) {
            _masterTraderManager = masterTraderManager;
            _activeAccountBusReceiver = activeAccountBusReceiver;

            _traderAccount = traderAccount.Value;
            _followerManager = followerManager;
            _queryProcessor = queryProcessor;
            _logger = logger;
            
        }

        public bool IsStarted { get; private set; }


        public async Task InitializeTradingsServiceAsync() {
            IsStarted = true;

            await _masterTraderManager.AppendUserAsync(_traderAccount.EmailAddress, _traderAccount.Password);
            var result = await _followerManager.GetActiveAccountNotOnFollowersTask();
            result.ForEach(y => _followerManager.AppendUser(y, _masterTraderManager.MasterOpenOrderStream));

            //auto subscribe to azure-bus
            _activeAccountBusReceiver
                .MessageObservable
                .Subscribe(x => {
                    if (x.IsActive) {
                        var iqAccount = _queryProcessor
                            .ProcessAsync(new GetIqAccountByIqUserIdQuery(x.UserId), CancellationToken.None)
                            .Result;

                        _logger.LogInformation($"Bus Received new Active account is {x.UserId}");
                        _followerManager.AppendUser(iqAccount, _masterTraderManager.MasterOpenOrderStream);
                    }
                    else
                    {
                        _logger.LogInformation($"Bus Received new InActive account is {x.UserId}");
                        _followerManager.RemoveByUserId(x.UserId);

                    }
                });
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