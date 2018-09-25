using Xunit;

namespace AiOption.Infrastructure.Integration.Steps.Autorization {

    public class SeedDefaultCustomerStep : IClassFixture<BaseSetup> {

        private readonly BaseSetup _baseSetup;

        public SeedDefaultCustomerStep(BaseSetup baseSetup) {
            _baseSetup = baseSetup;

        }

        //public async Task<CustomerReadModel> SeedDefaultCustomers() {

        //    var userManager = _baseSetup.Resolve<UserManager<CustomerDto>>();
        //    var result = await userManager.CreateAsync(new CustomerDto() {
        //        UserName = "m@email.com",
        //        Email = "m@email.com",
        //    }, "password1234");


        //}

    }

}