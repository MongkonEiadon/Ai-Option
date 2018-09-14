using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using AiOption.Application.ApplicationServices;
using AiOption.Domain.Customers.DomainServices;
using AiOption.Infrastructure.DataAccess.Identities;

using Xunit;

namespace AiOption.Infrastructure.Integration.UseCases.Authroization
{
    public class IdentityTest : IClassFixture<BaseSetup>
    {

        private readonly BaseSetup _baseSetup;

        public IdentityTest(BaseSetup baseSetup) {
            _baseSetup = baseSetup;

        }

        [Fact]
        public async  Task Test() {


            var result = await _baseSetup.Resolve<IAuthorizationDomainService>()
                .SigninWithPasswordAsync("m@email.com", "Code11054");

        }
    }
}
