using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using AiOption.Query;
using AiOption.Query.Customers;
using EventFlow.Queries;
using EventFlow.ReadStores;
using EventFlow.ReadStores.InMemory;
using EventFlow.ReadStores.InMemory.Queries;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AiOption.Tests.Integrations.Customers.Commands
{
    [TestFixture]
    public class TerminateCustomerCommandTest : IntegrationTest
    {
        [Test]
        public async Task Terminate()
        {
            // arrange
            await PublishAsync(new CustomerRegisterCommand("m@email.com", "passcode", "invitation"))
                .ConfigureAwait(true);

            var query = await QueryAsync(new QueryCustomerByEmailAddress(Email.New("m@email.com"))).ConfigureAwait(true);

            // act
            await PublishAsync(new RequestToTerminateCustomerCommand(query.Id));

            // assert
            var existingCustomer = await Resolve<IInMemoryReadStore<CustomerReadModel>>().FindAsync(x => x.UserName.EmailAddress.Equals("m@email.com"), CancellationToken.None);
            existingCustomer.Should().BeEmpty();
        }
    }
}
