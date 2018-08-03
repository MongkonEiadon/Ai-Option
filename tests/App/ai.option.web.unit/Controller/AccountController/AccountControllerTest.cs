using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ai.option.web.Controllers;
using ai.option.web.ViewModels;
using AutoMapper;
using iqoption.data.User;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using Xunit;


namespace ai.option.web.unit.Controller
{
    public class RegisterTest : BaseUnitTest {
        private IMapper _Mapper;
        public RegisterTest() {
            _Mapper = AutoSubstitute.SubstituteFor<IMapper>();
        }

        [Fact]
        public async Task Register_WithIncorrectInvitationCode_ResultShouldBeRegisterView() {

            //arrange
            var model = new LoginViewModel() {
                InvitationCode = "Invit"
            };

            //act
            var result = await AutoSubstitute.Resolve<AccountController>()
                .RegisterAsync(model) as ViewResult;

            //assert
            result.ShouldNotBeNull();
            result.ViewName.ShouldBe(nameof(AccountController.Register));
        }

       
    }
}
