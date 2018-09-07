using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace AiOption.Infrastructure.DataAccess.Identities
{
    public class CustomerIdentityRepository
    {

        private readonly UserManager<CustomerDto> _customerManager;
        private readonly SignInManager<CustomerDto> _signinManager;

        public CustomerIdentityRepository(UserManager<CustomerDto> customerManager, SignInManager<CustomerDto> signinManager) {
            _customerManager = customerManager;
            _signinManager = signinManager;
        }

        public async Task SigninWithPasswordAsync(string email, string password) {

            var si = await _signinManager.PasswordSignInAsync(email, password, true, false);
            if (si.Succeeded) {
                return;
            }
        }

        
    }
}
