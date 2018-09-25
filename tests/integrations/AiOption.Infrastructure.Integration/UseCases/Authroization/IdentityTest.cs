using System.Threading.Tasks;

using AiOption.Domain.Customers.DomainServices;

using Xunit;

namespace AiOption.Infrastructure.Integration.UseCases.Authroization {

    public class IdentityTest : IClassFixture<BaseSetup> {

        public IdentityTest(BaseSetup baseSetup) {
            _baseSetup = baseSetup;

        }

        private readonly BaseSetup _baseSetup;

        [Fact]
        public async Task Test() {


            var result = await _baseSetup.Resolve<IAuthorizationDomainService>()
                .SigninWithPasswordAsync("m@email.com", "Code11054");

        }

    }

}