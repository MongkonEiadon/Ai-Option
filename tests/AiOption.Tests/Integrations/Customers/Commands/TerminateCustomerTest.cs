using System.Linq;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using AiOption.Domain.IqAccounts;
using AiOption.TestCore;
using EventFlow.ReadStores.InMemory;
using NUnit.Framework;

namespace AiOption.Tests.Integrations.Customers.Commands
{
    [TestFixture]
    [Category(Category.Integrations)]
    public class TerminateCustomerTest : IntegrationTest
    {
        private IInMemoryReadStore<IqAccountReadModel> IqAccountReadModel => Resolve<IInMemoryReadStore<IqAccountReadModel>>();
        private IInMemoryReadStore<CustomerReadModel> CustomerReadModel => Resolve<IInMemoryReadStore<CustomerReadModel>>();


        [Test]
        public async Task TerminateCustomer_WithHasIqAccount_IqAccountShouldBeDeleted()
        {
            // arrange
            await PublishAsync(new CustomerRegisterCommand("m@email.com", "passcode", "invitation"));
            var id = (await FindAsync<CustomerReadModel>(x => true)).FirstOrDefault()?.AggregateId;

            await PublishAsync(new CreateNewIqAccountCommand(CustomerId.With(id), Email.New("m@email.com"), Password.New("passcode")));

            // act
            //await PublishAsync(new TerminateRequestCommand(CustomerId.With(id)));

            // assert

        }
    }
}