using AiOption.Domain.Common;
using FluentAssertions;
using Xunit;

namespace AiOption.Tests.Units.Commons
{
    public class PasswordTest
    {
        [Fact]
        public void TestEquals()
        {
            // arrange
            var password = new Password("PlainText");
            var password2 = new Password("PlainText");

            // act
            var result = password.Equals(password2);

            // arrange
            result.Should().BeTrue();
        }

        [Fact]
        public void TestValidate()
        {
            // arrange
            var password = new Password("PlainText");
            var password2 = new Password("PlainText");

            // act
            var result = password.IsPasswordMatched(password2);

            //act
            result.Should().BeTrue();
        }

        [Fact]
        public void TestValue()
        {
            // arrange
            var password = new Password("PlainText");

            // assert
            password.Value.Should().NotBe("PlainText");
        }

        [Fact]
        public void TestWith()
        {
            // arrange
            var password = Password.With("ua2X5lU4XbfM5+/PUYJKUkErvvUQAZZE6+IQ+DTB+o4=");

            //assert
            password.DecryptPassword().Should().Be("PlainText");
        }
    }
}