using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace iqoption.trading.services.Middleware {
    public class TradingPersistenceServiceMiddleware {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly TradingPersistenceService _tradingPersistenceService;

        public TradingPersistenceServiceMiddleware(
            TradingPersistenceService tradingPersistenceService,
            ILogger logger, RequestDelegate next) {
            _tradingPersistenceService = tradingPersistenceService;
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext) {
            _logger.LogTrace($"{nameof(TradingPersistenceServiceMiddleware)} received!");

            await InitialServiceAsync();


            await _next.Invoke(httpContext);
        }

        public async Task InitialServiceAsync() {
            if (!_tradingPersistenceService.IsStarted)
                await _tradingPersistenceService.InitializeTradingsServiceAsync();
        }
    }
}