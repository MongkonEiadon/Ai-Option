using System;
using System.Net;
using System.Threading.Tasks;
using ai.option.web.Controllers;
using ai.option.web.ViewModels;
using Castle.Core.Logging;
using iqoption.domain.IqOption.Command;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Xunit;

namespace ai.option.web.unit.Controller {
    
    public class IqOptionControllerTest : BaseUnitTest {
        
        public ILogger<IqOptionController> _Logger { get; set; }

        public IqOptionControllerTest() {
            _Logger = AutoSubstitute.Resolve<ILogger<IqOptionController>>();
        }
        


        [Fact]
        public async Task GetTokenAsync_WithEmailOrPasswordEmpty_ResponseWithOk() {

            //arrange
            var requestVm = new IqOptionRequestViewModel();

            //act
            var result = await AutoSubstitute.Resolve<IqOptionController>()
                .GetTokenAsync(requestVm) as OkResult;

            //assert
            result.ShouldNotBeNull();
        }


        [Fact]
        public async Task GetTokenAsync_WithLoginCommandFailed_BadRequestReturn() {

            ////arrange
            //var requestVm = new IqOptionRequestViewModel {EmailAddress = "email", Password = "password"};
            //_Session.Send(Arg.Any<LoginCommand>())
            //    .Returns(Task.FromResult(new LoginCommandResult("", false, "AnyError")));

            ////act
            //var result = await AutoSubstitute.Resolve<IqOptionController>().GetTokenAsync(requestVm)
            //    as BadRequestObjectResult;

            ////assert
            //result.ShouldNotBeNull();
        }


        [Fact]
        public async Task GetTokenAsync_WithValidToken_StoreSsidCommandHandlerMustSend() {

            //arrange
            //var requestVm = new IqOptionRequestViewModel { EmailAddress = "email", Password = "password" };
            //_Session.Send(Arg.Any<LoginCommand>())
            //    .Returns(Task.FromResult(new LoginCommandResult("Ssid", true, "")));

            ////act
            //var result = await AutoSubstitute.Resolve<IqOptionController>().GetTokenAsync(requestVm)
            //    as BadRequestObjectResult;

            ////assert
            //await _Session.Received().Send(Arg.Is<StoreSsidCommand>(x => x.Ssid == "Ssid" && x.EmailAddress == "email"));

        }


    }
}