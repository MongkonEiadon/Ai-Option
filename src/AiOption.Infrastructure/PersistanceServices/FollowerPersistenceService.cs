using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Application.Bus;
using AiOption.Application.Persistences;
using AiOption.Domain.Accounts;
using AiOption.Domain.IqOptions;
using EventFlow;
using EventFlow.Queries;

using IqOptionApi.ws;

using Serilog;

namespace AiOption.Infrastructure.PersistanceServices {

    public class FollowerPersistenceService : ApplicationTradingPersistence, IFollowerPersistenceService {

        private readonly IBusReceiver<ActiveAccountQueue, Account> _activeAccountQueue;

        private readonly ILogger _logger;
        private readonly IQueryProcessor _queryProcessor;
        private readonly ITraderPersistenceService _tradersPersistenceService;


        public FollowerPersistenceService(
            IBusReceiver<ActiveAccountQueue, IqAccount> activeAccountQueue,
            ICommandBus commandBus,
            IQueryProcessor queryProcessor,
            ITraderPersistenceService tradersPersistenceService,
            ILogger logger) : base(commandBus) {
            _activeAccountQueue = activeAccountQueue;
            _queryProcessor = queryProcessor;
            _tradersPersistenceService = tradersPersistenceService;
            _logger = logger;


            _activeAccountQueue.MessageObservable.Subscribe(x => {

                if (!x.IsActive)
                    RemoveAccountTask(x);
                else
                    _queryProcessor.ProcessAsync(new GetAccountByAccoutIdQuery(x.UserId), CancellationToken.None)
                        .ContinueWith(t => {
                            if (t.Result != null) AppendAccountTask(t.Result).ConfigureAwait(false);
                        });
            });
        }

        public override Task<IDisposable> Handle(Account account) {
            var client = new IqOptionWebSocketClient(account.SecuredToken);
            var dispose = _tradersPersistenceService
                .TraderOpenPositionStream
                .Subscribe(x => {
                    client.BuyAsync(x.ActiveId, (int) x.Sum * account.GetMultipler(), x.Direction, x.Expired)
                        .ContinueWith(t => {

                            var r = t.Result;
                            _logger.Information(
                                $"Follower\t {account.Level}\t {r.Act} {r.Value} {r.Direction} {r.Exp} ");
                        });
                });

            return Task.FromResult(dispose);
        }

        public override Task<IEnumerable<Account>> GetAccounts() {
            return _queryProcessor.ProcessAsync(new GetFollowerAccountToOpenTradingsQuery(), CancellationToken.None);

        }

    }

}