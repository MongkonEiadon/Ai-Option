using AiOption.Infrastructure.Mappings;

using AutoMapper;

using Xunit;

namespace AiOption.Infrastructure.Test.Mappings {

    public class ValidationTests {

        [Fact]
        public void ValidateProfile() {

            Mapper.Initialize(c => {
                c.AddProfile<CustomersProfile>();
                c.AddProfile<IqAccountsProfile>();
            });

            Mapper.AssertConfigurationIsValid();
        }

    }

}