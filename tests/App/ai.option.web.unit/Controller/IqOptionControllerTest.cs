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


     
    }
}