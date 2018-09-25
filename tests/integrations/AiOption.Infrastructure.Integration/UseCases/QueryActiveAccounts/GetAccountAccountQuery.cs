using System.Linq;
using System.Threading.Tasks;

using AiOption.Application.Repositories.ReadOnly;
using AiOption.Infrastructure.DataAccess;
using AiOption.TestCore.FixtureSetups;

using Xunit;

namespace AiOption.Infrastructure.Integration.UseCases.QueryActiveAccounts {

    public class GetAccountAccountQuery : IClassFixture<BaseSetup>,
        IClassFixture<DataContextSetupCleaner<AiOptionDbContext>> {

        public GetAccountAccountQuery(BaseSetup setup, DataContextSetupCleaner<AiOptionDbContext> dbCleaner) {
            _setup = setup;
            _dbCleaner = dbCleaner;

        }

        private readonly BaseSetup _setup;
        private readonly DataContextSetupCleaner<AiOptionDbContext> _dbCleaner;


        [Fact]
        public async Task GetActiveAccount_With2ExsitingAnd2NotActive_ActiveAccountWith2RecordsReturned() {

            //arrange
            using (var db = _setup.Resolve<AiOptionDbContext>()) {
                var accounts = new[] {
                    new IqAccountDto {Id = 1, IsActive = true},
                    new IqAccountDto {Id = 2, IsActive = true},
                    new IqAccountDto {Id = 3, IsActive = false},
                    new IqAccountDto {Id = 4, IsActive = false}
                };


                db.IqAccounts.AddRange(accounts);
                db.SaveChanges();
            }


            //act
            var result = await _setup.Resolve<IIqOptionAccountReadOnlyRepository>()
                .GetActiveAccountForOpenTradingsAsync();


            //arrange
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }


        [Fact]
        public async Task GetActiveAccount_WithNotExsiting_EnumerableEmptyOfAccountReturn() {

            //arrange

            //act
            var result = await _setup.Resolve<IIqOptionAccountReadOnlyRepository>()
                .GetActiveAccountForOpenTradingsAsync();


            //arrange
            Assert.NotNull(result);
            Assert.Empty(result);
        }

    }

}