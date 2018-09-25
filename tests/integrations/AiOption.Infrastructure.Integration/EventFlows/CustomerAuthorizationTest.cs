using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;

using EventFlow;
using EventFlow.Queries;

using Xunit;

namespace AiOption.Infrastructure.Integration.EventFlows {

    public class CustomerAuthorizationTest : IClassFixture<BaseSetup> {

        public CustomerAuthorizationTest(BaseSetup baseSetup) {
            _baseSetup = baseSetup;
        }

        private readonly BaseSetup _baseSetup;


        [Fact]
        public async Task CustomerRegister_WithValidInvitationCode_NewCustomerRegistered() {

            var bus = _baseSetup.Resolve<ICommandBus>();
            var id = CustomerId.New;
            await bus.PublishAsync(new CustomerRegisterCommand(id, new CustomerReadModel {
                EmailAddress = "m@email.com",
                //Password = "Code11054",
                //InvitationCode = "Invitation"
            }), CancellationToken.None);


            var query = _baseSetup.Resolve<IQueryProcessor>();
            var resultModel =
                await query.ProcessAsync(new ReadModelByIdQuery<CustomerReadModel>(id), CancellationToken.None);


        }

    }

}