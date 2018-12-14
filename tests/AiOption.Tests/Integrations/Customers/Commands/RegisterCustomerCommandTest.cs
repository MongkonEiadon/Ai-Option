using System.Threading;
using System.Threading.Tasks;
using AiOption.Application.ApplicationServices;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using AiOption.Domain.IqAccounts;
using AiOption.TestCore;
using EventFlow.ReadStores.InMemory;
using FluentAssertions;
using NUnit.Framework;

namespace AiOption.Tests.Integrations.Customers.Commands
{
    [Category(Category.Integrations)]
    public class RegisterCustomerCommandTest : IntegrationTest
    {
        [SetUp]
        public void SetupTerminateCustomerCommandTest()
        {
            base.EventFlowOptions
                .RegisterServices(r =>
                    r.Register(typeof(ICustomerProcessManagerService), typeof(CustomerProcessManagerService)));
        }

        [Test]
        public async Task CustomerRegisterCommand_WithOneRequest_CustomerShouldCreated()
        {
            // act
            await PublishAsync(new CustomerRegisterCommand("m@email.com", "passcode", "invite"));

            // assert
            var result = await Resolve<IInMemoryReadStore<CustomerReadModel>>().FindAsync(x => true, CancellationToken.None);
            result.Count.Should().Be(1);
        }
    }
}