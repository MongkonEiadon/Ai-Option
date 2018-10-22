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
            return options
                .ConfigureMongoDb(
                    "mongodb://localhost:C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==@localhost:10255/admin?ssl=true")
                .AddDefaults(typeof(ReadStoreMongoExtensions).Assembly)
                .UseEventStoreOnMongoDb()
                .UseSnapshotStoreOnMongoDb();//.UseReadModelOnMongoDb<CustomerReadModelOnMongo>();
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
