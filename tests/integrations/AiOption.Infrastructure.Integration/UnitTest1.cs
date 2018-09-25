using AiOption.Infrastructure.DataAccess;
using AiOption.TestCore.FixtureSetups;

using Xunit;

namespace AiOption.Infrastructure.Integration {

    public class AccountRepositoryTest : IClassFixture<DataContextSetupCleaner<AiOptionDbContext>>,
        IClassFixture<BaseSetup> {

        private readonly DataContextSetupCleaner<AiOptionDbContext> _aioptiondbContextSetupCleaner;
        private readonly BaseSetup _baseSetup;


        public AccountRepositoryTest(DataContextSetupCleaner<AiOptionDbContext> aioptiondbContextSetupCleaner,
            BaseSetup baseSetup) {
            _aioptiondbContextSetupCleaner = aioptiondbContextSetupCleaner;
            _baseSetup = baseSetup;
        }

    }

}