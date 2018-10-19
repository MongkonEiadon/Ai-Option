using System;
using AiOption.Infrastructure.ReadStores.Mongo.ReadModels;
using EventFlow;
using EventFlow.Extensions;
using EventFlow.MongoDB.Extensions;
using EventFlow.MongoDB.ReadStores;

namespace AiOption.Infrastructure.ReadStores.Mongo
{
    public static class ReadStoreMongoExtensions
    {
        public static IEventFlowOptions UseReadStoreOnMongoDb(this IEventFlowOptions options)
        {
            return options.ConfigureMongoDb("")
                .AddDefaults(typeof(ReadStoreMongoExtensions).Assembly)
                .UseReadModelOnMongoDb<CustomerReadModelOnMongo>();
        }

        public static IEventFlowOptions UseReadModelOnMongoDb<TReadModel>(this IEventFlowOptions options) 
            where TReadModel : class, IMongoDbReadModel, new()
        {
            return options
                .UseMongoDbReadModel<TReadModel>();
        }

        public static IEventFlowOptions UseEventStoreOnMongoDb(this IEventFlowOptions options) {
            return options.UseMongoDbEventStore();
        }

        public static IEventFlowOptions UseSnapshotStoreOnMongoDb(this IEventFlowOptions options) {
            return options.UseMongoDbSnapshotStore();
        }
    }
}
