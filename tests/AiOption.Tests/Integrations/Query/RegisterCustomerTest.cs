using System;
using System.Threading.Tasks;
using AiOption.Domain.Customers;
using AiOption.Query.Customers;
using AiOption.TestCore;
using EventFlow.Exceptions;
using FluentAssertions;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AiOption.Tests.Integrations.Query
{
    [Trait("Category", Category.Integrations)]
    public class QueryCustomerTests : IntegrationTest
    {

        [Fact]
        public async Task QueryEmptyReadModel_WithNotThrowOnNotFound_ExceptionShouldNotThrow()
        {
            // act
            var result = await QueryAsync(new QueryCustomerById(A<CustomerId>(), false));

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public void QueryEmptyReadModel_WithThrowOnNotFound_ExceptionShouldThrew()
        {
            //act
            Action action = () => { var a = QueryAsync(A<QueryCustomerById>()).Result; };
            
            // assert
            action.Should().Throw<DomainError>();
        }

    }
}
