using AiOption.Domain.Customers.Commands;
using AiOption.TestCore;
using Xunit;

namespace AiOption.Tests.Units.Domain.Customers.Commands
{
    public class LoginCommandHandlerTest : Test
    {
        [Fact]
        public void TestCommand()
        {
            var handler = A<LoginCommandHandler>();
        }
    }
}