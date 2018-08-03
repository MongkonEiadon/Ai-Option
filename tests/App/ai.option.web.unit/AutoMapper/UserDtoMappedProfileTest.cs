using ai.option.web.AutoMapper;
using ai.option.web.ViewModels;
using AutoMapper;
using iqoption.data.User;
using Shouldly;
using Xunit;

namespace ai.option.web.unit.AutoMapper {
    public class UserDtoMappedProfileTest : BaseUnitTest {


        public UserDtoMappedProfileTest()
        {
            Mapper.Reset();
            Mapper.Initialize(c => c.AddProfile<UserDtoMappedProfile>());

            Mapper.AssertConfigurationIsValid();
        }

        [Fact]
        public void MapLoginViewModel_WithInviationCode_InviationCodeMustSet() {

            //arrange
            var model = new LoginViewModel() {
                InvitationCode = "Invit"
            };

            //act
            var result = Mapper.Map<UserDto>(model);

            //assert
            result.InviationCode.ShouldBe("Invit");
        }

        [Fact]
        public void MapLoginViewModel_WithEmailAddress_UserNameMustSet() {
            //arrange
            var model = new LoginViewModel()
            {
                EmailAddress = "m@email.com"
            };

            //act
            var result = Mapper.Map<UserDto>(model);

            //assert
            result.UserName.ShouldBe("m@email.com");
        }

        [Fact]
        public void MapLoginViewModel_WithEmailAddress_EmailShouldSet()
        {
            //arrange
            var model = new LoginViewModel()
            {
                EmailAddress = "m@email.com"
            };

            //act
            var result = Mapper.Map<UserDto>(model);

            //assert
            result.Email.ShouldBe("m@email.com");
        }
    }
}