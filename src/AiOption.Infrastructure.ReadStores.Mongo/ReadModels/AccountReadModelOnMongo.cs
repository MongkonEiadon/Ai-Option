using AiOption.Domain.IqAccounts;
using EventFlow.MongoDB.ReadStores;
using EventFlow.MongoDB.ReadStores.Attributes;

namespace AiOption.Infrastructure.ReadStores.Mongo.ReadModels
{
    [MongoDbCollectionName("accounts")]
    public class AccountReadModelOnMongo : IqAccountReadModel, IMongoDbReadModel
    {
        public string _id => AggregateId;
        public long? _version
        {
            get => Version;
            set => Version = value;
        }
    }
}