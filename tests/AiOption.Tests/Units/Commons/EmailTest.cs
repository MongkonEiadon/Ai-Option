using System;
using System.Collections.Generic;
using System.Text;
using AiOption.Domain.Common;
using EventFlow.Exceptions;
using FluentAssertions;
using Xunit;

namespace AiOption.Tests.Units.Commons
{
    public class EmailTest 
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("Invalid")]
        public void TestEmailWithInValid(string invalid)
        {
            Action a = () => { new Email(invalid); };

            a.Should().Throw<DomainError>();
        }
    }
}
