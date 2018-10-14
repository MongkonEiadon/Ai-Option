using System;
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
            Action a = () =>
            {
                var email = new Email(invalid);
            };

            a.Should().Throw<DomainError>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("Invalid")]
        public void CreateWithNewTest(string invalid)
        {
            Action a = () => { Email.New(invalid); };

            a.Should().Throw<DomainError>();
        }

        [Fact]
        public void CreateEmailWithValid()
        {
            var result = Email.New("m@email.com");

            result.EmailAddress.Should().Be("m@email.com");
        }
    }
}