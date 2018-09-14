using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Application.ApplicationServices;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;

using EventFlow;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AiOption.WebPortal.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AuthController : Controller {

        private readonly ICommandBus _commandBus;


        private readonly AppSettings _appSettings;

        public AuthController(IOptions<AppSettings> appSettings,
            ICommandBus commandBus) {
            _commandBus = commandBus;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> AuthenticateAsync(string emailAddress, string password, string invitationCode) {

            var id = CustomerId.New;
            await _commandBus.PublishAsync(new CustomerRegisterCommand(id, new CustomerState() {
                EmailAddress = emailAddress,
                Id = id.GetGuid(),
                Password = password,
                InvitationCode = invitationCode
            }), CancellationToken.None);


            await Task.Delay(1000);

            return Ok();
        }
    }
}
