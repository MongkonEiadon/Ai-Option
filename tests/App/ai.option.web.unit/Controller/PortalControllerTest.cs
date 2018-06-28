using System.Threading.Tasks;
using ai.option.web.Controllers;
using ai.option.web.ViewModels;
using iqoption.data.Model;
using iqoption.data.Services;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace ai.option.web.unit.Controller {
    [TestFixture]
    public class PortalControllerTest : BaseUnitTest {
        [SetUp]
        public void Setup() {
            _UserService = AutoSubstitute.Resolve<IUserService>();
            _iqOptionAccountService = AutoSubstitute.Resolve<IIqOptionAccountService>();
        }

        private IUserService _UserService;
        private IIqOptionAccountService _iqOptionAccountService;


        [Test]
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