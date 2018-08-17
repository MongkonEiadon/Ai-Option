using System;
using System.Collections.Generic;
using System.Text;

using AutoMapper;

using Xunit;

namespace AiOption.Infrastructure.Test.Mappings
{
    public class ValidationTests
    {
        [Fact]
        public void ValidateProfile() {

            Mapper.Initialize(c => {
                c.AddProfile<Infrastructure.Mappings.CustomersProfile>();
                c.AddProfile<Infrastructure.Mappings.IqAccountsMappingProfile>();
            });

            Mapper.AssertConfigurationIsValid();


        }
    }
}
