using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

using AiOption.Application;
using AiOption.Infrastructure.DataAccess;
using AiOption.Infrastructure.Modules;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using AutofacContrib.NSubstitute;

using EventFlow;
using EventFlow.MsSql;
using EventFlow.MsSql.EventStores;
using EventFlow.MsSql.SnapshotStores;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AiOption.Infrastructure.Integration
{

    public class BaseSetup
    {
        public IServiceProvider Container { get; }

        public BaseSetup()
        {

            var services = new ServiceCollection();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<DomainModule>();
            containerBuilder.RegisterModule<InfrastructureModule>();
            containerBuilder.RegisterModule<ApplicationModule>();
            containerBuilder.RegisterModule<BusModule>();
            services.AddSingleton<AiOptionDbContext>(new AiOptionDbContext());
            services.AddTransient<IDbConnection>(x => new SqlConnection(config.GetConnectionString("aioptiondb")));
            services.AddEntityFrameworkSqlServer();
            services.AddInfrastructureConfiguration();
            services.AddEventFlowInfrastructure(config, containerBuilder);

            containerBuilder.Populate(services);

            var build = containerBuilder.Build();
            Container = new AutofacServiceProvider(build);

            //ensure database
            var db = build.Resolve<AiOptionDbContext>();
            db.Database.EnsureCreated();

            var msSqlDatabaseMigrator = build.Resolve<IMsSqlDatabaseMigrator>();
            EventFlowEventStoresMsSql.MigrateDatabase(msSqlDatabaseMigrator);
            EventFlowSnapshotStoresMsSql.MigrateDatabase(msSqlDatabaseMigrator);

        }


        public TService Resolve<TService>()
        {
            return Container.GetService<TService>();
        }

    }

}