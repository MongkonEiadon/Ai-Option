using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using AiOption.Domain.Customers;
using AiOption.Infrastructure.DataAccess;

using Microsoft.AspNetCore.Identity;

using Xunit;

namespace AiOption.Infrastructure.Integration.Steps.Autorization
{
    public class SeedDefaultCustomerStep : IClassFixture<BaseSetup>
    {

        private readonly BaseSetup _baseSetup;

        public SeedDefaultCustomerStep(BaseSetup baseSetup) {
            _baseSetup = baseSetup;

        }

        //public async Task<CustomerState> SeedDefaultCustomers() {

        //    var userManager = _baseSetup.Resolve<UserManager<CustomerDto>>();
        //    var result = await userManager.CreateAsync(new CustomerDto() {
        //        UserName = "m@email.com",
        //        Email = "m@email.com",
        //    }, "password1234");

            
        //}
    }
}
