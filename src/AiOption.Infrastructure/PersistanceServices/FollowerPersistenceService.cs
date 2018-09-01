using System;
using System.Threading.Tasks;

using AiOption.Application.Persistences;
using AiOption.Domain.Accounts;

using EventFlow;

using IqOptionApi.ws;

using Serilog;

namespace AiOption.Infrastructure.PersistanceServices {

    public interface IFollowerPersistenceService {

    }


    public class FollowerPersistenceService : ApplicationTradingPersistence, IFollowerPersistenceService {

        private readonly ILogger _logger;

        private readonly ITraderPersistenceService _tradersPersistenceService;


        public FollowerPersistenceService(ICommandBus commandBus,
            ITraderPersistenceService tradersPersistenceService,
            ILogger logger) : base(commandBus) {
            _tradersPersistenceService = tradersPersistenceService;
            _logger = logger;
        }

        public override Task<IDisposable> Handle(Account account) {
            var client = new IqOptionWebSocketClient(account.SecuredToken);
            var dispose = _tradersPersistenceService
                .TraderOpenPositionStream
                .Subscribe(x => {
                    client.BuyAsync(x.ActiveId, (int) x.Sum, x.Direction, x.Expired)
                        .ContinueWith(t => {
                            var r = t.Result;
                            _logger.Information($"FollowerOpen\t {r.Act} {r.Value} {r.Direction} {r.Exp}");
                        });
                });

            return Task.FromResult(dispose);
        }

    }

}