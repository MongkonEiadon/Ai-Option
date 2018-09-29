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


    }

}