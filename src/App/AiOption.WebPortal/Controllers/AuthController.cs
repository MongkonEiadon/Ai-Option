using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AiOption.Application.ApplicationServices;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AiOption.WebPortal.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AuthController : Controller {

        private readonly IApplicationAuthorizationServices _applicationAuthorizationServices;

        private readonly AppSettings _appSettings;

        public AuthController(IOptions<AppSettings> appSettings, 
            IApplicationAuthorizationServices applicationAuthorizationServices) {
            _applicationAuthorizationServices = applicationAuthorizationServices;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> AuthenticateAsync(string emailAddress, string password) {


            var secret = _appSettings.Secret;


            return Ok();
        }
    }
}
