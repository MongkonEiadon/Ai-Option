using System;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Customers;
using AiOption.Domain.IqAccounts;
using AiOption.TestCore;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Core;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Extensions;
using EventFlow.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace AiOption.Tests.Integrations
{
    public class IntegrationTest : Test
    {
        public IntegrationTest()
        {
            var services = new ServiceCollection();

            services.AddEventFlow(x =>
                x.AddDomain()
                    .UseInMemoryReadStoreFor<CustomerReadModel>()
                    .UseInMemoryReadStoreFor<IqAccountReadModel>()
                    .UseInMemorySnapshotStore()
                    .UsingDomainInmemoryReadStore());

            Resolver = services.BuildServiceProvider();
        }

        public IServiceProvider Resolver { get; }


        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            return Resolver.GetService<IQueryProcessor>()
                .ProcessAsync(query, CancellationToken.None);
        }

        public Task<TResult> PublishAsync<TAggregate, TIdentity, TResult>(
            ICommand<TAggregate, TIdentity, TResult> command)
            where TAggregate : IAggregateRoot<TIdentity>
            where TIdentity : IIdentity
            where TResult : IExecutionResult
        {
            return Resolver.GetService<ICommandBus>()
                .PublishAsync(command, CancellationToken.None);
        }

        public T Resolve<T>()
        {
            return Resolver.GetService<T>();
        }
    }
}