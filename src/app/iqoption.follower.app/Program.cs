using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using iqoption.trading.services;
using iqoption.trading.services.Middleware;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using iqoption.core;
using iqoption.core.Extensions;
using iqoption.data;
using iqoption.data.AutofacModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Configuration;
using Serilog.Events;
using Serilog;
using Serilog.Configuration;

namespace iqoption.follower.app
{
    public class Program
    {
        static void Main(string[] args) {

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

    public class Startup
    {
        IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = configBuilder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services) {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DataAutofacModule>();

           
            services.AddSingleton<IConfigurationRoot>(Configuration);

            services
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<AiOptionContext>(op => {
                    
                    op.UseLazyLoadingProxies()
                        .UseLoggerFactory(new NullLoggerFactory())
                        .UseSqlServer(Configuration.GetConnectionString("aioptiondb"));
                })
                .AddAutoMapper()
                .AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());



            //logging



            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            var loggerFactory = new LoggerFactory().AddSerilog(Log.Logger);

            services.AddSingleton(loggerFactory);
            services.AddSingleton(loggerFactory.CreateLogger(nameof(data.Startup)));
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));




            //add trandings services
            services
                .AddTradingServices();

            builder.Populate(services);

            var container = builder.Build();

            return container.Resolve<IServiceProvider>();
        }


    }

    public static class SeriLogMixins {

        private static IServiceCollection AddSeriLogs(this IServiceCollection This) {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();


            return This;
        }
    }
}
