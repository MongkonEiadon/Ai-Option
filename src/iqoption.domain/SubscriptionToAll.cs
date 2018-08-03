using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Subscribers;
using Microsoft.Extensions.Logging;

namespace iqoption.domain {
    public class SubscriptionToAll : ISubscribeSynchronousToAll {
        private readonly ILogger<SubscriptionToAll> _logger;

        public SubscriptionToAll(ILogger<SubscriptionToAll> logger) {
            _logger = logger;
        }

        public Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken) {

            _logger.LogDebug($"{domainEvents.Last().ToString()}");
            return Task.CompletedTask;
        }
    }

    public class TraderAccountConfiguration {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}