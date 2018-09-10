using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;

using EventFlow;
using EventFlow.Queries;

using Xunit;

namespace AiOption.Infrastructure.Integration.EventFlows {

    public class CustomerAuthorizationTest : IClassFixture<BaseSetup> {

        private readonly BaseSetup _baseSetup;

        public CustomerAuthorizationTest(BaseSetup baseSetup) {
            _baseSetup = baseSetup;
        }


        [Fact]
        public async Task Test() {


            var bus = _baseSetup.Resolve<ICommandBus>();
            var id = CustomerId.New;
            var result = await bus.PublishAsync(new CustomerLoginCommand(id, "email", "password"), CancellationToken.None);



            var query = _baseSetup.Resolve<IQueryProcessor>();
        }

        [Fact]
        public async Task CustomerRegister_WithValidInvitationCode_NewCustomerRegistered() {

            var bus = _baseSetup.Resolve<ICommandBus>();
            var id = CustomerId.New;
            var result = await bus.PublishAsync(new CustomerRegisterCommand(id, new NewCustomer() {
                EmailAddress = "m@email.com",
                Password = "Code11054",
                InvitationCode = "Invitation"
            }), CancellationToken.None);

            

        }

    }
}
