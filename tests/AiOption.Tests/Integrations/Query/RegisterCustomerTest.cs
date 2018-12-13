using System;
using System.Threading.Tasks;
using AiOption.Domain.Customers;
using AiOption.Query.Customers;
using AiOption.TestCore;
using EventFlow.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace AiOption.Tests.Integrations.Query
{
    [Category(Category.Integrations)]
    public class QueryCustomerTests : IntegrationTest
    {
        [Test]
        public async Task QueryEmptyReadModel_WithNotThrowOnNotFound_ExceptionShouldNotThrow()
        {
            // act
            var result = await QueryAsync(new QueryCustomerById(A<CustomerId>(), false));

            // assert
            result.Should().BeNull();
        }

        [Test]
        public void QueryEmptyReadModel_WithThrowOnNotFound_ExceptionShouldThrew()
        {
            //act
            Action action = () =>
            {
                var a = QueryAsync(A<QueryCustomerById>()).Result;
            };

            // assert
            action.Should().Throw<DomainError>();
        }
    }
}