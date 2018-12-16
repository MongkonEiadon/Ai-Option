using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using AiOption.TestCore;
using EventFlow.EventStores;
using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using FluentAssertions;
using NUnit.Framework;

namespace AiOption.Tests.Integrations.Customers.Commands
{
    [TestFixture]
    [Category(Category.Integrations)]
    public class ChangeLevelCommandTest : IntegrationTest
    {
        public ChangeLevelCommandTest()
        {
            Fixture.Customize<CustomerId>(c => c.FromFactory(() => CustomerId.New));
        }

        [Test]
        [TestCaseSource(typeof(UserLevelTestCaseSources))]
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

        [Test]
        [TestCaseSource(typeof(UserLevelTestCaseSources))]
        public async Task ChangeLevelCommand_WithAllUserLevel_ShouldNotMoreThanOneReadStoreCreated(UserLevel level)
        {
            // arrange
            var id = A<CustomerId>();
            CreateReadModel<CustomerReadModel>(id.Value);

            // act
            await PublishAsync(new ChangeLevelCommand(id, new Level(level)));

            // assert
            var result = await Resolve<IInMemoryReadStore<CustomerReadModel>>()
                .FindAsync(rm => true, CancellationToken.None);
            result.Count.Should().Be(1);
        }

        [Test]
        [TestCaseSource(typeof(UserLevelTestCaseSources))]
        public async Task ChangeLevelCommand_WithAllUserLevel_OneEventShouldRosed(UserLevel level)
        {
            // arrange
            var id = A<CustomerId>();
            CreateReadModel<CustomerReadModel>(id.Value);

            // act
            await PublishAsync(new ChangeLevelCommand(id, new Level(level)));

            // assert
            var result = await Resolve<IEventStore>()
                .LoadEventsAsync<CustomerAggregate, CustomerId>(id, CancellationToken.None);
            result.Count.Should().Be(1);
        }
    }
}