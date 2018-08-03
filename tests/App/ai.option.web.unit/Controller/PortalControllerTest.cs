using System.Threading;
using System.Threading.Tasks;
using ai.option.web.Controllers;
using ai.option.web.ViewModels;
using EventFlow;
using EventFlow.Commands;
using iqoption.data.IqOptionAccount;
using iqoption.data.Services;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Command;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using Xunit;

namespace ai.option.web.unit.Controller {
    public class PortalControllerTest : BaseUnitTest {
        public PortalControllerTest() {
            _UserService = AutoSubstitute.Resolve<IUserService>();
            _iqOptionAccountService = AutoSubstitute.Resolve<IIqOptionAccountService>();
            _commangBus = AutoSubstitute.Resolve<ICommandBus>();
        }

        private IUserService _UserService;
        private readonly IIqOptionAccountService _iqOptionAccountService;
        private readonly ICommandBus _commangBus;


        [Fact]
        public async Task AddIqOptionAccountAsync_WithAnyModel_CommandBusShouldReceived() {

            //arrange
            var model = new IqOptionRequestViewModel() {
                EmailAddress = "m@email.com",
                Password = "password",
            };

            _commangBus.PublishAsync(
                    Arg.Is<CreateOrUpdateIqAccountCommand>(x => x.AccountDetail.IqOptionUserName.Equals("m@email.com")),
                    Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(new IqAccount() {IsSuccess = true}));


            //act
            var result = await AutoSubstitute.Resolve<PortalController>()
                .AddIqOptionAccountAsync(model);


            //assert
            await _commangBus.Received()
                .PublishAsync(Arg.Any<CreateOrUpdateIqAccountCommand>(), Arg.Any<CancellationToken>());

        }

        [Fact]
        public async Task AddIqOptionAccount_WithNotExistingAccount_NewAccountGenerated() {
            //arrange
            var model = new IqOptionRequestViewModel() {
                EmailAddress = "m@email.com",
                Password = "password",
            };

            _commangBus.PublishAsync(
                    Arg.Any<CreateOrUpdateIqAccountCommand>(),
                    Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(new IqAccount() {IsSuccess = true}));


            //act
            var result = await AutoSubstitute.Resolve<PortalController>()
                .AddIqOptionAccountAsync(model) as RedirectToActionResult;


            //assert
            result.ShouldNotBeNull();
            result.ActionName.ShouldContain(nameof(PortalController.IqOptionAccount));
        }

        [Fact]
        public async Task AddIqOptionAccountAsync_WithExistingAccount_UpdateShouldReceived() {
            //arrange
            var model = new IqOptionRequestViewModel() {
                EmailAddress = "m@email.com",
                Password = "password",
            };

            _commangBus.PublishAsync(
                    Arg.Any<CreateOrUpdateIqAccountCommand>(),
                    Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(new IqAccount() {IsSuccess = false}));


            //act
            var result = await AutoSubstitute.Resolve<PortalController>()
                .AddIqOptionAccountAsync(model) as BadRequestObjectResult;


            //assert
            result.ShouldNotBeNull();
            result.Value.ShouldBe("ไม่สามารถเพิ่มบัญชีได้");
        }
    }
}