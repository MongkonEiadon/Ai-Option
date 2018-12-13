using System;
using System.Diagnostics;
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
using EventFlow.ReadStores;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AiOption.Tests.Integrations
{
    public class IntegrationTest : Test
    {
        protected IEventFlowOptions EventFlowOptions { get; private set; }
        
        [SetUp]
        public void  SetupIntegrationTest()
        {
            EventFlowOptions = EventFlow.EventFlowOptions.New
                .AddDomain()
                    .UseInMemoryReadStoreFor<CustomerReadModel>()
                    .UseInMemoryReadStoreFor<IqAccountReadModel>()
                    .UseInMemorySnapshotStore()
                    .UsingDomainInMemoryReadStore();

            LazyResolver = new Lazy<IRootResolver>(() => EventFlowOptions.CreateResolver());
        }

        private Lazy<IRootResolver> LazyResolver { get; set; }
        public IRootResolver Resolver => LazyResolver.Value;


        [DebuggerStepThrough]
        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            return Resolver.Resolve<IQueryProcessor>()
                .ProcessAsync(query, CancellationToken.None);
        }


        [DebuggerStepThrough]
        public Task<TResult> PublishAsync<TAggregate, TIdentity, TResult>(
            ICommand<TAggregate, TIdentity, TResult> command)
            where TAggregate : IAggregateRoot<TIdentity>
            where TIdentity : IIdentity
            where TResult : IExecutionResult
        {
            return Resolver.Resolve<ICommandBus>()
                .PublishAsync(command, CancellationToken.None);
        }

        [DebuggerStepThrough]
        public T Resolve<T>()
        {
            return Resolver.Resolve<T>();
        }

        [DebuggerStepThrough]
        protected TReadModel CreateReadModel<TReadModel>(string id)
            where TReadModel : IReadModel
        {
            var rm = Resolve<IReadModelFactory<TReadModel>>().CreateAsync(id, CancellationToken.None).Result;

            return rm;
        }


    }
}