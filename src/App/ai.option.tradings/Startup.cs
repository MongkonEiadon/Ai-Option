using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventFlow.Autofac.Extensions;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Extensions;
using EventFlow.MsSql;
using EventFlow.MsSql.Extensions;
using FluentValidation.AspNetCore;
using iqoption.apiservice.DependencyModule;
using iqoption.core.Extensions;
using iqoption.data;
using iqoption.data.DependencyModule;
using iqoption.domain;
using iqoption.trading.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;

namespace ai.option.tradings {
    public class Startup
    {
        public Startup()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = configBuilder.Build();
        }

        private IConfigurationRoot Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DataAutofacModule>();


            services.AddSingleton(Configuration);

            var config = new DbContextOptionsBuilder()
                .UseSqlServer(Configuration.GetConnectionString("aioptiondb"))
                .UseLoggerFactory(new NullLoggerFactory());
            
            services
                .AddDbContext<DbContext, AiOptionContext>(op => {
                    op.UseLazyLoadingProxies()
                        .UseLoggerFactory(new NullLoggerFactory())
                        .UseSqlServer(Configuration.GetConnectionString("aioptiondb"));
                })
                
                .Configure<TraderAccountConfiguration>(Configuration.GetSection(nameof(TraderAccountConfiguration)))


                .AddAutoMapper()
                .AddMvc()
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling =
                        ReferenceLoopHandling.Ignore
                )
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());


            //logging
            services
                .AddLogging(c => {
                    c.AddDebug();
                    c.AddConsole(cfg => { cfg.DisableColors = false; });
                })
                .AddSingleton<ILoggerFactory, LoggerFactory>()
                .AddSingleton<ILogger>(c => c.GetService<ILogger<Startup>>())
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>)) // Add first my already configured instance

                //trandings services
                .AddEventFlow(o => {
                    o.UseAutofacContainerBuilder(builder)
                        .Configure(c => { c.IsAsynchronousSubscribersEnabled = true; })
                        .ConfigureMsSql(MsSqlConfiguration.New.SetConnectionString(Configuration.GetConnectionString("aioptiondb")))
                        .UseMsSqlSnapshotStore()
                        .UseMssqlEventStore()
                        .UseInMemorySnapshotStore()
                        .AddEventFlowForData()
                        .UseEventFlowOptionsForApiService()
                        .UseEventFlowInDomain();
                })
                .AddTradingServices();

            
            builder.Populate(services);

            var container = builder.Build();


            return container.Resolve<IServiceProvider>();
        }
    }
}