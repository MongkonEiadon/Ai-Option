using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iqoption.trading.services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace iqoption.web.MiddleWare
{
    public class TradersFollowerMiddleWare
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly ITradingPersistenceService _tradingPersistenceService;
        private bool IsTradingStart = false;

        public TradersFollowerMiddleWare(ILogger logger, RequestDelegate next, ITradingPersistenceService tradingPersistenceService) {
            _logger = logger;
            _next = next;
            _tradingPersistenceService = tradingPersistenceService;
        }





        public async Task Invoke(HttpContext httpContext) {
            await SetupTradingSubscriptions();
            await _next.Invoke(httpContext);
        }

        public async Task<bool> SetupTradingSubscriptions() {
            if (IsTradingStart == false) {
                await _tradingPersistenceService.InitializeTradingsServiceAsync();
                IsTradingStart = true;
            }

            return IsTradingStart;
        }
    }

}
