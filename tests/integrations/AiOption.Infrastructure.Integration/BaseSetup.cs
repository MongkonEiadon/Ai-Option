using System;
using System.Data;
using System.Data.SqlClient;

using AiOption.Application;
using AiOption.Infrastructure.DataAccess;
using AiOption.Infrastructure.Modules;

using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AiOption.Infrastructure.Integration {

    public class BaseSetup {

        public BaseSetup() {

            var services = new ServiceCollection();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<DomainModule>();
            containerBuilder.RegisterModule<InfrastructureModule>();
            containerBuilder.RegisterModule<ApplicationModule>();
            containerBuilder.RegisterModule<BusModule>();

            services.AddSingleton(new AiOptionDbContext());
            services.AddTransient<IDbConnection>(x => new SqlConnection(config.GetConnectionString("aioptiondb")));
            services.AddEntityFrameworkSqlServer();
            services.AddInfrastructureConfiguration();
            services.AddEventFlowInfrastructure(config, containerBuilder);

            containerBuilder.Populate(services);

            var build = containerBuilder.Build();
            Container = new AutofacServiceProvider(build);

        }

        public IServiceProvider Container { get; }


        public TService Resolve<TService>() {
            return Container.GetService<TService>();
        }

    }

}