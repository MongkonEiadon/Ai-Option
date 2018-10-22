using System;
using System.Linq;
using AiOption.Application;
using AiOption.Infrastructure.Modules;
using AiOption.Infrastructure.ReadStores;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using EventFlow.Autofac.Extensions;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Serilog.ILogger;

namespace AiOption.Tradings
{
    public class Startup
    {
        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            var builder = new ContainerBuilder();

            // Once you've registered everything in the ServiceCollection, call
            // Populate to bring those registrations into Autofac. This is
            // just like a foreach over the list of things in the collection
            // to add them to Autofac.
            ConfigureContainer(builder);


            //infra-configuration
            services.AddMvc();
            services.AddInfrastructureConfiguration();
            services.AddLogging(c => c.AddConsole());
            services.AddEfConfigurationDomain(Configuration);

            builder.Populate(services);

            //event flows
            services.AddEventFlow(cfg =>
                cfg.UseAutofacContainerBuilder(builder)
                    .Configure(c =>
                    {
                        c.IsAsynchronousSubscribersEnabled = true;
                        c.ThrowSubscriberExceptions = true;
                    })
                    .AddDomain()
                    .AddApplication()
                    .AddInfrastructureReadStores()
            );

            var container = builder.Build();
            var resolver =  new AutofacServiceProvider(container);
            return resolver;
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var logger = new LoggerConfiguration()

                //.ReadFrom.Configuration(Configuration)
                .WriteTo.ColoredConsole()
                .CreateLogger();

            builder.RegisterModule(new ConfigurationModule(Configuration));
            builder.RegisterModule<BusModule>();
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<ApplicationModule>();


            builder.Register(c => logger).As<ILogger>().SingleInstance();
        }
    }
}