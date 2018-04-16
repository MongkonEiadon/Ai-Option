using System;
using System.Diagnostics;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using iqoption.domain.Orders;
using iqoption.servicebus.Position;
using iqoptionapi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrderDirection = iqoptionapi.OrderDirection;

namespace iqoption.leaders.app {
    public class Startup {
        private readonly IqOptionConfiguration _iqOptionConfiguration;
        private readonly ILogger _logger;
        private readonly IOrderSender _orderSender;
        private readonly ITraderManager _traderManager;

        IConfigurationRoot Configuration { get; }


        public Startup(
            ILogger<Startup> logger, 
            IOptions<IqOptionConfiguration> configurationOptions,
            ITraderManager traderManager
            
            ) {

            _iqOptionConfiguration = configurationOptions.Value;
            _logger = logger;
            _traderManager = traderManager;
        }

        public  async Task Run()
        {
            _logger.LogInformation("Start hosting");


            await _traderManager.BeginSubscribeTradersTask();

        }

        
    }
}