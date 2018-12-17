using System;
using System.Net;
using EventFlow;
using EventFlow.EventStores.EventStore.Extensions;
using EventFlow.Extensions;
using EventFlow.MetadataProviders;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;

namespace AiOption.Infrastructure.EventStores
{
    public static class EventStoreConfigurations
    {
        public static IEventFlowOptions UsingAiOptionEventStores(this IEventFlowOptions Options)
        {
            var uri = new Uri("http://d006629:2113/");


            var conn = ConnectionSettings.Create()
                .EnableVerboseLogging()
                .KeepReconnecting()
                .KeepRetrying()
                .SetDefaultUserCredentials(new UserCredentials("user_integration_test", "Code11054"))
                .Build();

            return Options
                .AddMetadataProvider<AddGuidMetadataProvider>()
                .UseEventStoreEventStore();
        } 

    }
}
