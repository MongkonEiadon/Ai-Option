using AiOption.Domain.Customers.Commands;
using AiOption.TestCore;
using NUnit.Framework;

namespace AiOption.Tests.Units.Domain.Customers.Commands
{
    public class LoginCommandHandlerTest : Test
    {
        [Test]
        public void TestCommand()
        {
            var handler = A<LoginCommandHandler>();
            
            Assert.IsNotNull(handler);
        }
    }
}