using AiOption.Domain.Customers;
using AiOption.Domain.IqAccounts;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AiOption.Tests.Integrations
{
    internal class IntegrationTest 
    {
        public IntegrationTest()
        {
            var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

            var services = new ServiceCollection();


            services.AddEventFlow(x =>
                x.AddDomain()
                    .UseServiceCollection(services)
                    .UseInMemoryReadStoreFor<CustomerReadModel>()
                    .UseInMemoryReadStoreFor<IqAccountReadModel>()
                    .UseInMemorySnapshotStore()
                    .UsingDomainInmemoryReadStore());
        }
    }
}
