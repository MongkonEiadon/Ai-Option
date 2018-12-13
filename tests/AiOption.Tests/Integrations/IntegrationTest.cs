using System;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using AiOption.Domain.Customers.Events;
using AiOption.Domain.IqAccounts;
using AiOption.TestCore;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Configuration;
using EventFlow.Core;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Extensions;
using EventFlow.Queries;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AiOption.Tests.Integrations
{
    public class IntegrationTest : Test
    {
        [SetUp]
        public void SetupIntegrationTest()
        {
            var ops = EventFlowOptions.New
                .AddDomain()
                    .UseInMemoryReadStoreFor<CustomerReadModel>()
                    .UseInMemoryReadStoreFor<IqAccountReadModel>()
                    .UseInMemorySnapshotStore()
                    .UsingDomainInMemoryReadStore();

            LazyResolver = new Lazy<IRootResolver>(() => ops.CreateResolver());
        }

        private Lazy<IRootResolver> LazyResolver { get; set; }
        public IRootResolver Resolver => LazyResolver.Value;


        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            return Resolver.Resolve<IQueryProcessor>()
                .ProcessAsync(query, CancellationToken.None);
        }

        public Task<TResult> PublishAsync<TAggregate, TIdentity, TResult>(
            ICommand<TAggregate, TIdentity, TResult> command)
            where TAggregate : IAggregateRoot<TIdentity>
            where TIdentity : IIdentity
            where TResult : IExecutionResult
        {
            return Resolver.Resolve<ICommandBus>()
                .PublishAsync(command, CancellationToken.None);
        }

        public T Resolve<T>()
        {
            return Resolver.Resolve<T>();
        }
    }
}