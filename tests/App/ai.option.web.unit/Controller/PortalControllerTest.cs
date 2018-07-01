using System.Threading.Tasks;
using ai.option.web.Controllers;
using ai.option.web.ViewModels;
using iqoption.data.IqOptionAccount;
using iqoption.data.Services;
using NSubstitute;
using Shouldly;
using Xunit;

namespace ai.option.web.unit.Controller {
    public class PortalControllerTest : BaseUnitTest {
        public PortalControllerTest() {
            _UserService = AutoSubstitute.Resolve<IUserService>();
            _iqOptionAccountService = AutoSubstitute.Resolve<IIqOptionAccountService>();
        }

        private IUserService _UserService;
        private readonly IIqOptionAccountService _iqOptionAccountService;

        [Fact]
        public async Task AddIqOptionAccount_WithNotExistingAccount_NewAccountGenerated() {
            //arrange
            var accountDto = new IqOptionAccountDto();
            var model = new IqOptionRequestViewModel {
                EmailAddress = "m@email.com",
                Password = "password",
                ProfileResponseViewModel = new IqOptionProfileResponseViewModel {
                    UserId = 1234
                }
            };

            _iqOptionAccountService.GetAccountByUserIdAsync(Arg.Any<long>())
                .Returns(Task.FromResult(default(IqOptionAccountDto)));

            //act
            var result = await AutoSubstitute.Resolve<PortalController>()
                .AddIqOptionAccountAsync(model);


            result.ShouldNotBeNull();
            await _iqOptionAccountService.Received().CreateAccountTask(accountDto);
        }


        [Fact]
        public async Task AddIqOptionAccountAsync_WithExistingAccount_UpdateShouldReceived() {
            //arrange
            var accountDto = new IqOptionAccountDto();
            var model = new IqOptionRequestViewModel {
                EmailAddress = "m@email.com",
                Password = "password",
                ProfileResponseViewModel = new IqOptionProfileResponseViewModel {
                    UserId = 1234
                }
            };

            _iqOptionAccountService.GetAccountByUserIdAsync(1234)
                .Returns(Task.FromResult(accountDto));


            //act
            var result = await AutoSubstitute.Resolve<PortalController>()
                .AddIqOptionAccountAsync(model);


            result.ShouldNotBeNull();
            await _iqOptionAccountService
                .Received(1)
                .UpdateAccountTask(Arg.Is<IqOptionAccountDto>(dto => dto != null));
        }
    }
}