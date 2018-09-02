using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Application.Bus;
using AiOption.Application.Persistences;
using AiOption.Domain.Accounts;
using AiOption.Domain.IqAccounts;
using AiOption.Domain.IqAccounts.Events;
using AiOption.Domain.IqAccounts.Queries;

using EventFlow;
using EventFlow.Queries;

using IqOptionApi.Models;
using IqOptionApi.ws;

using Serilog;

namespace AiOption.Infrastructure.PersistanceServices {

    public class TraderPersistenceService : ApplicationTradingPersistence, ITraderPersistenceService {

        private readonly ICommandBus _commandBus;
        private readonly ILogger _logger;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IBusReceiver<ActiveTradersQueue, IqAccountStatusChanged> _tradersBusReceiver;
        private readonly Subject<InfoData> _tradersOpenPositionSubject = new Subject<InfoData>();

        public TraderPersistenceService(ICommandBus commandBus, ILogger logger, IQueryProcessor queryProcessor) : base(commandBus) {
            _commandBus = commandBus;
            _logger = logger;
            _queryProcessor = queryProcessor;

            TraderOpenPositionStream.Subscribe(x => {
                _logger.Information(
                    $"TraderOpen\t {x.ActiveId} {x.Sum} {x.Direction} {x.Expired}\t account [{x.UserId}]");
            });

        }

        public IObservable<InfoData> TraderOpenPositionStream => _tradersOpenPositionSubject.Publish().RefCount();

        public override Task<IDisposable> Handle(Account account) {
            var client = new IqOptionWebSocketClient(account.SecuredToken);
            var dispose = client.InfoDataObservable
                .Where(x => x.Any() && x[0].Win == "equal")
                .Select(x => x[0])
                .Subscribe(x => { _tradersOpenPositionSubject.OnNext(x); });

            return Task.FromResult(dispose);
        }

        public override Task<IEnumerable<Account>> GetAccounts() {
            return _queryProcessor.ProcessAsync(new GetTraderAccountToOpenTradingsQuery(), CancellationToken.None);
        }

    }

}