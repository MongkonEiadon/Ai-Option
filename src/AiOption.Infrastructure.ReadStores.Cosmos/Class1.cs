using System;
using EventFlow;

namespace AiOption.Infrastructure.ReadStores.Cosmos
{
    public static class ReadStoreForCosmosExtensions
    {
        public static IEventFlowOptions UseReadStoreInCosmos(this IEventFlowOptions options)
        {
            return options;
        }
    }
}
