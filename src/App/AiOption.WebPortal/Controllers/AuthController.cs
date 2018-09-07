using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AiOption.Application.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AiOption.WebPortal.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AuthController : Controller {

        private readonly ICustomerAccountServices _customerAccountServices;

        private readonly AppSettings _appSettings;

        public AuthController(IOptions<AppSettings> appSettings, 
            ICustomerAccountServices customerAccountServices) {
            _customerAccountServices = customerAccountServices;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> AuthenticateAsync(string emailAddress, string password) {

            var customer = await _customerAccountServices.LoginAsync(emailAddress, password);

            return Ok();
        }
    }
}
