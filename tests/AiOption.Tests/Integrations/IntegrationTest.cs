using AiOption.Infrasturcture.ReadStores;
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
            services
                .AddEfConfigurationDomain(config);
        }
    }
}
