using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iqoption.leaders.app;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace iqoption.web.MiddleWare
{
    public class TradersFollowerMiddleWare
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly ITraderManager _traderManager;
        private bool IsTradingStart = false;

        public TradersFollowerMiddleWare(ILogger logger, RequestDelegate next, ITraderManager traderManager) {
            _logger = logger;
            _next = next;
            _traderManager = traderManager;
        }





        public async Task Invoke(HttpContext httpContext) {
            await SetupTradingSubscriptions();
            await _next.Invoke(httpContext);
        }

        public async Task<bool> SetupTradingSubscriptions() {
            if (IsTradingStart == false) {
                await _traderManager.BeginSubscribeTradersTask();
                IsTradingStart = true;
            }

            return IsTradingStart;
        }
    }

}
