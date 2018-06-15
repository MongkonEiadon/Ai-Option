using System;
using System.Collections.Generic;
using System.Text;
using ai.option.web.AutoMapper;
using ai.option.web.ViewModels;
using AutoMapper;
using iqoption.data.Model;
using NUnit.Framework;
using Shouldly;

namespace ai.option.web.unit.AutoMapper
{
    [TestFixture]
    public class IqOptionAccountDtoMappedProfileTest
    {
        [OneTimeSetUp]
        public void Setup() {

            Mapper.Initialize(c => { c.AddProfile<IqOptionAccountDtoMappedProfile>(); });

        }

        [Test]
        public void Mapper_WithEmailAndPassword_PropertiesShouldSet() {
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
        [Test]
        public void Mapper_WithUserId_PropertiesShouldSet() {
            //arrange
            var model = new IqOptionRequestViewModel();
            model.EmailAddress = "m@email.com";
            model.Password = "password";
            model.ProfileResponseViewModel = new IqOptionProfileResponseViewModel() {
                UserId = 1234
            };

            //act
            var result = Mapper.Map<IqOptionAccountDto>(model);
            
            //assert
            Mapper.AssertConfigurationIsValid();
            result.IqOptionUserId.ShouldBe(1234);

        }
    }
}
