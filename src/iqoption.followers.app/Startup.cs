using System;
using System.Threading.Tasks;
using iqoption.domain.Orders;
using iqoption.servicebus.Position;
using iqoptionapi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace iqoption.followers.app {
    public class Startup {
        private readonly IqOptionConfiguration _iqOptionConfiguration;
        private readonly ILogger _logger;
        private readonly OrderReceiver _orderReceiver;

        public Startup(ILogger<Startup> logger, 
            IOptions<IqOptionConfiguration> configurationOptions, 
            OrderReceiver orderReceiver) {
            _iqOptionConfiguration = configurationOptions.Value;
            _logger = logger;
            _orderReceiver = orderReceiver;
        }

        public async Task Run()
        {
            
           



        }
    }
}