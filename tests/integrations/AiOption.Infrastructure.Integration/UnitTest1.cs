using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

using AiOption.Application.QueryHandlers;
using AiOption.Application.Repositories.ReadOnly;
using AiOption.Infrastructure.DataAccess;
using AiOption.Infrastructure.DataAccess.Repositories;
using AiOption.TestCore;
using AiOption.TestCore.FixtureSetups;

using Autofac;

using EventFlow;
using EventFlow.Autofac.Extensions;
using EventFlow.Extensions;
using EventFlow.Queries;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Xunit;

namespace AiOption.Infrastructure.Integration {

    public class AccountRepositoryTest : IClassFixture<DataContextSetupCleaner<AiOptionDbContext>>, IClassFixture<BaseSetup> {

        private readonly DataContextSetupCleaner<AiOptionDbContext> _aioptiondbContextSetupCleaner;
        private readonly BaseSetup _baseSetup;


        public AccountRepositoryTest(DataContextSetupCleaner<AiOptionDbContext> aioptiondbContextSetupCleaner, BaseSetup baseSetup) {
            _aioptiondbContextSetupCleaner = aioptiondbContextSetupCleaner;
            _baseSetup = baseSetup;
        }


        [Fact]
        public async Task GetActiveAccountTest() {

           

        }


    }

}