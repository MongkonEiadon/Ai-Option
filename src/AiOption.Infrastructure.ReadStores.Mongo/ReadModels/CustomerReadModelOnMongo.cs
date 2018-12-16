using System;
using System.Collections.Generic;
using AiOption.Domain.Customers;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.MongoDB.ReadStores.Attributes;
using EventFlow.ReadStores;

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

        public string Id { get; }
    }

    public interface ICustomerReadModelLocator : IReadModelLocator
    {
    }

    public class CustomerReadModelLocator : ICustomerReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            throw new NotImplementedException();
        }
    }
}