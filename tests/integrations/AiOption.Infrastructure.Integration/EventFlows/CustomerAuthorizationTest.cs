using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Application.Repositories;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using AiOption.Infrastructure.DataAccess;

using EventFlow;
using EventFlow.Queries;
using EventFlow.ReadStores;

using Xunit;

namespace AiOption.Infrastructure.Integration.EventFlows {

    public class CustomerAuthorizationTest : IClassFixture<BaseSetup> {

        private readonly BaseSetup _baseSetup;

        public CustomerAuthorizationTest(BaseSetup baseSetup) {
            _baseSetup = baseSetup;
        }


        [Fact]
        public async Task CustomerRegister_WithValidInvitationCode_NewCustomerRegistered() {

            var bus = _baseSetup.Resolve<ICommandBus>();
            var id = CustomerId.New;
            await bus.PublishAsync(new CustomerRegisterCommand(id, new CustomerState() {
                EmailAddress = "m@email.com",
                Password = "Code11054",
                InvitationCode = "Invitation"
            }), CancellationToken.None);



            var query = _baseSetup.Resolve<IQueryProcessor>();
            var resultModel = await query.ProcessAsync(new ReadModelByIdQuery<CustomerState>(id), CancellationToken.None);
 

        }

    }
}
