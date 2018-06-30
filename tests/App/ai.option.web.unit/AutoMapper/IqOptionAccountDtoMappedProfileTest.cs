using System;
using ai.option.web.AutoMapper;
using ai.option.web.ViewModels;
using AutoMapper;
using iqoption.data.Model;
using NUnit.Framework;
using Shouldly;
using Xunit;

namespace ai.option.web.unit.AutoMapper {


    public class IqOptionAccountDtoMappedProfileTest : IClassFixture<BaseUnitTest>, IDisposable {

        public IqOptionAccountDtoMappedProfileTest() {

            Mapper.Reset();
            Mapper.Initialize(c => c.AddProfile<IqOptionAccountDtoMappedProfile>());

            Mapper.AssertConfigurationIsValid();
        }

        [Fact]
        public void Mapper_WithEmailAndPassword_PropertiesShouldSet() {
            Mapper.Reset();
            Mapper.Initialize(c => c.AddProfile<IqOptionAccountDtoMappedProfile>());
            
            
            //arrange
            var model = new IqOptionRequestViewModel();
            model.EmailAddress = "m@email.com";
            model.Password = "password";

            //act
            var result = Mapper.Map<IqOptionAccountDto>(model);

            //assert
            Mapper.AssertConfigurationIsValid();
            result.IqOptionUserName.ShouldBe("m@email.com");
            result.Password.ShouldBe("password");
        }

        [Fact]
        public void Mapper_WithUserId_PropertiesShouldSet() {
            //arrange
            var model = new IqOptionRequestViewModel();
            model.EmailAddress = "m@email.com";
            model.Password = "password";
            model.ProfileResponseViewModel = new IqOptionProfileResponseViewModel {
                UserId = 1234
            };

            //act
            var result = Mapper.Map<IqOptionAccountDto>(model);

            //assert
            Mapper.AssertConfigurationIsValid();
            result.IqOptionUserId.ShouldBe(1234);
        }

        public void Dispose() {
        }
    }
}