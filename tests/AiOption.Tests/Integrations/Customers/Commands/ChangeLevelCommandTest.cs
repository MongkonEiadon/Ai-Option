using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using AiOption.Domain.Customers.Events;
using AiOption.TestCore;
using EventFlow.Aggregates;
using EventFlow.Core.VersionedTypes;
using EventFlow.EventStores;
using EventFlow.Queries;
using EventFlow.ReadStores;
using EventFlow.ReadStores.InMemory;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;

namespace AiOption.Tests.Integrations.Customers.Commands
{
    [Category(Category.Integrations)]
    public class ChangeLevelCommandTest : IntegrationTest
    {
        public ChangeLevelCommandTest()
        {
            Fixture.Customize<CustomerId>(c => c.FromFactory(() => CustomerId.New));
        }

        [TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.UserLevels))]
        public async Task ChangeLevelCommand_WithAllUserLevel_UserLevelShouldApplied(UserLevel level)
        {
            // arrange
            var id = A<CustomerId>();
            CreateReadModel<CustomerReadModel>(id.Value);

            // act
            await PublishAsync(new ChangeLevelCommand(id, new Level(level)));
            
            // assert
            var result = await QueryAsync(new ReadModelByIdQuery<CustomerReadModel>(id));
            result.Level.Value.Should().Be(level);
        }

        [TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.UserLevels))]
        public async Task ChangeLevelCommand_WithAllUserLevel_ShouldNotMoreThanOneReadStoreCreated(UserLevel level)
        {
            // arrange
            var id = A<CustomerId>();
            CreateReadModel<CustomerReadModel>(id.Value);

            // act
            await PublishAsync(new ChangeLevelCommand(id, new Level(level)));

            // assert
            var result = await Resolve<IInMemoryReadStore<CustomerReadModel>>().FindAsync(rm => true, CancellationToken.None);
            result.Count.Should().Be(1);
        }

        [TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.UserLevels))]
        public async Task ChangeLevelCommand_WithAllUserLevel_OneEventShouldRised(UserLevel level)
        {
            // arrange
            var id = A<CustomerId>();
            CreateReadModel<CustomerReadModel>(id.Value);

            // act
            await PublishAsync(new ChangeLevelCommand(id, new Level(level)));

            // assert
            var result = await Resolve<IEventStore>().LoadEventsAsync<CustomerAggregate, CustomerId>(id, CancellationToken.None);
            result.Count.Should().Be(1);
        }
    }
}
