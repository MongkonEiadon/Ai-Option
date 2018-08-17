using System;
using System.Threading.Tasks;

using AiOption.Infrastructure.DataAccess;
using AiOption.Infrastructure.Modules;

using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Serilog;
using Serilog.Core;

namespace AiOption.Tradings {

    public class Startup {
        public IConfigurationRoot Configuration { get; }

        public Startup() {

            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            var builder = new ContainerBuilder();

            // Once you've registered everything in the ServiceCollection, call
            // Populate to bring those registrations into Autofac. This is
            // just like a foreach over the list of things in the collection
            // to add them to Autofac.
            builder.Populate(services);
            ConfigureContainer(builder);


            //eventflows
            services.AddEventFlowModule(builder, Configuration);

            //efs
            services.AddEfConfigurationDomain(Configuration);


            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }

        public void ConfigureContainer(ContainerBuilder builder) {

            

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            builder.RegisterModule(new ConfigurationModule(Configuration));
            builder.RegisterModule<BusModule>();
            builder.RegisterModule<InfrastructureModule>();


            builder.Register(c => logger).As<ILogger>().SingleInstance();



        }

    }

}