using System;
using AiOption.Domain.Customers;
using AiOption.Infrasturcture.ReadStores;
using AiOption.Query;
using EventFlow;
using Xunit;

namespace AiOption.Infrastructure.ReadStores.EF.Tests
{
    public class DependencyInjectionTests
    {
        [Fact]
        public void Test1()
        {
            using (var resolver = EventFlowOptions.New
                .ConfigureInfrastructureReadModelStore()
                .CreateResolver())
            {
                var readstore = resolver.Resolve<ISearchableReadModelStore<CustomerReadModel>>();

            }


        }
    }
}
