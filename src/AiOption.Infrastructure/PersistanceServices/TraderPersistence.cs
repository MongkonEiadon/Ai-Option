using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

using AiOption.Application.Bus;
using AiOption.Application.Persistences;
using AiOption.Domain.Accounts;
using AiOption.Domain.IqOptions.Events;

using EventFlow;
using EventFlow.Queries;

using IqOptionApi.Models;
using IqOptionApi.ws;

using Serilog;

namespace AiOption.Infrastructure.PersistanceServices {

    public interface ITraderPersistenceService {

        IObservable<InfoData> TraderOpenPositionStream { get; }

    }


    public class TraderPersistenceService : ApplicationTradingPersistence, ITraderPersistenceService {

        private readonly ICommandBus _commandBus;
        private readonly ILogger _logger;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IBusReceiver<ActiveTradersQueue, StatusChangeEventItem> _tradersBusReceiver;
        private readonly Subject<InfoData> _tradersOpenPositionSubject = new Subject<InfoData>();

        public TraderPersistenceService(ICommandBus commandBus, ILogger logger) : base(commandBus) {
            _commandBus = commandBus;
            _logger = logger;

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

    }

}