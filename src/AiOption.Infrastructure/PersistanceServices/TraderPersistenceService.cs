namespace AiOption.Infrastructure.PersistanceServices
{
    //public class TraderPersistenceService : ApplicationTradingPersistence, ITraderPersistenceService {

    //    private readonly ICommandBus _commandBus;
    //    private readonly ILogger _logger;
    //    private readonly IQueryProcessor _queryProcessor;
    //    private readonly IBusReceiver<ActiveTradersQueue, IqAccountStatusChanged> _tradersBusReceiver;
    //    private readonly Subject<InfoData> _tradersOpenPositionSubject = new Subject<InfoData>();

    //    public TraderPersistenceService(ICommandBus commandBus, ILogger logger, IQueryProcessor queryProcessor) :
    //        base(commandBus) {
    //        _commandBus = commandBus;
    //        _logger = logger;
    //        _queryProcessor = queryProcessor;

    //        TraderOpenPositionStream.Subscribe(x => {
    //            _logger.Information(
    //                $"TraderOpen\t {x.ActiveId} {x.Sum} {x.Direction} {x.Expired}\t account [{x.UserId}]");
    //        });

    //    }

    //    public IObservable<InfoData> TraderOpenPositionStream => _tradersOpenPositionSubject.Publish().RefCount();

    //    public override Task<IDisposable> Handle(Account account) {
    //        var client = new IqOptionWebSocketClient(account.SecuredToken);
    //        var dispose = client.InfoDataObservable
    //            .Where(x => x.Any() && x[0].Win == "equal")
    //            .Select(x => x[0])
    //            .Subscribe(x => { _tradersOpenPositionSubject.OnNext(x); });

    //        return Task.FromResult(dispose);
    //    }

    //    public override Task<IEnumerable<Account>> GetAccounts() {
    //        return _queryProcessor.ProcessAsync(new GetTraderAccountToOpenTradingsQuery(), CancellationToken.None);
    //    }

    //}
}