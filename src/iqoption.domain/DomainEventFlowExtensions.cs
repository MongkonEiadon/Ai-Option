using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using EventFlow;
using EventFlow.Extensions;

namespace iqoption.domain
{
    public static class DomainEventFlowExtensions {

        public static IEventFlowOptions UseEventFlowInDomain(this IEventFlowOptions options) {
            options.AddDefaults(typeof(DomainEventFlowExtensions).Assembly);

            return options;
        }
    }
}
