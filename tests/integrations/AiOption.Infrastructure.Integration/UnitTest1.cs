using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

using AiOption.Application.QueryHandlers;
using AiOption.Application.Repositories.ReadOnly;
using AiOption.Infrastructure.DataAccess;
using AiOption.Infrastructure.DataAccess.Repositories;
using AiOption.Infrastructure.Modules;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using AutofacContrib.NSubstitute;

using EventFlow;
using EventFlow.Autofac.Extensions;
using EventFlow.Extensions;
using EventFlow.Queries;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace AiOption.Infrastructure.Integration
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1() {

            var services = new ServiceCollection();


            var container = new ContainerBuilder();

            services.AddInfrastructureConfiguration();
            services.AddEfConfigurationDomain(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());


            var autoSub = new AutoSubstitute(builder => {
                builder.RegisterType<AiOptionDbContext>();
                builder.RegisterModule<DomainModule>();
                builder.RegisterModule<InfrastructureModule>();
                builder.Populate(services);
            });



            using (var resolver = EventFlowOptions.New
                .UseAutofacContainerBuilder(autoSub.Container as ContainerBuilder)
                .AddQueryHandlers(typeof(IqOptionQueryHandlers))
                
                .CreateResolver()) {
                var query = resolver.Resolve<IQueryProcessor>();




            }
        }
    }


    public class TestSetUp {
        

        public TestSetUp() {
            
        }

    }
}
