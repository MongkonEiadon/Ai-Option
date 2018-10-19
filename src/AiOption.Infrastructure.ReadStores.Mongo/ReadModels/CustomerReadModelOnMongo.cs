using System;
using System.Collections.Generic;
using System.Text;
using AiOption.Domain.Customers;
using EventFlow.MongoDB.ReadStores;
using EventFlow.MongoDB.ReadStores.Attributes;

namespace AiOption.Infrastructure.ReadStores.Mongo.ReadModels
{
    [MongoDbCollectionName("customers")]
    public class CustomerReadModelOnMongo : CustomerReadModel, 
        IMongoDbReadModel
    {
        public string _id => AggregateId;
        public long? _version
        {
            get => Version;
            set => Version = value;
        }
    }
}
