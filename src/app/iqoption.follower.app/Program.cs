using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventFlow.Autofac.Extensions;
using EventFlow.DependencyInjection.Extensions;
using FluentValidation.AspNetCore;
using iqoption.apiservice.DependencyModule;
using iqoption.core.Extensions;
using iqoption.data;
using iqoption.data.DependencyModule;
using iqoption.trading.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;

namespace iqoption.follower.app {
    public class Program {
        private static void Main(string[] args) {
            TradingPersistenceService tradingPersistenceService = null;

            try {
                Console.WriteLine("iqoption-follower-starting");

                var services = new ServiceCollection();

                //    // Startup.cs finally :)
                var startup = new Startup();
                var serviceProvider = startup.ConfigureServices(services);


                tradingPersistenceService = serviceProvider.GetService<TradingPersistenceService>();
                tradingPersistenceService.InitializeTradingsServiceAsync().Wait();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally {
                Console.ReadLine();
            }
        }
    }

    public class Startup {
        public Startup() {
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = configBuilder.Build();
        }

        private IConfigurationRoot Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services) {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DataAutofacModule>();


            services.AddSingleton(Configuration);

            services
                .AddDbContext<AiOptionContext>(op => {
                    op.UseLazyLoadingProxies()
                        .UseLoggerFactory(new NullLoggerFactory())
                        .UseSqlServer(Configuration.GetConnectionString("aioptiondb"));
                })
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
                    o.UseAutofacContainerBuilder(builder);
                    o.AddEventFlowForData();
                })
                .AddTradingServices();


            builder
                .RegisterModule<DataAutofacModule>()
             builder.Populate(services);

            var container = builder.Build();


            return container.Resolve<IServiceProvider>();
        }
    }
}