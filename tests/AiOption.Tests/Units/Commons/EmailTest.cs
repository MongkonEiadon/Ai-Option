using System;
using AiOption.Domain.Common;
using EventFlow.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace AiOption.Tests.Units.Commons
{
    [TestFixture]
    public class EmailTest
    {
        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("Invalid")]
        public void TestEmailWithInValid(string invalid)
        {
            Action a = () =>
            {
                var email = new Email(invalid);
            };

            a.Should().Throw<DomainError>();
        }

        [Theory]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("Invalid")]
        public void CreateWithNewTest(string invalid)
        {
            Action a = () => { Email.New(invalid); };

            a.Should().Throw<DomainError>();
        }

        [Test]
        public void CreateEmailWithValid()
        {
            var result = Email.New("m@email.com");

            result.EmailAddress.Should().Be("m@email.com");
        }

        [Test]
        [TestCase("m@email.com", "M@email.com")]
        [TestCase("m2@email.com", "M2@email.com")]
        public void CompareEmail_WithValidCases_AllMustEqual(string email1, string email2)
        {
            // arrange
            var e1 = Email.New(email1);
            var e2 = Email.New(email2);

            // assert
            e1.Equals(e2).Should().BeTrue();
        }
    }
}