using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using AiOption.TestCore;
using EventFlow.Queries;
using Xunit;

namespace AiOption.Tests.Units.Domain.Customers.Commands
{
    
    public class LoginCommandHandlerTest : Test {


        [Fact]
        public async Task TestFor()
        {

            var query = Resolve<IQueryProcessor>();
            var id = CustomerId.New;

            await A<LoginCommandHandler>()
                .ExecuteAsync(new CustomerAggregate(id),
                    new LoginCommand(id, new User(""), new Password("")), CancellationToken.None);
        }
    }
}
