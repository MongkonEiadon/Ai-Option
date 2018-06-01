using System;
using FluentValidation.AspNetCore;
using iqoption.trading.services;
using iqoption.trading.services.Middleware;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
                startup.ConfigureServices(services);
                var serviceProvider = services.BuildServiceProvider();


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
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationRoot>(Configuration);

            services
                .AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            //logging
            services
                .AddSingleton<ILoggerFactory, LoggerFactory>()
                .AddSingleton<ILogger>(c => c.GetService<ILogger<Startup>>())
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>)); // Add first my already configured instance


            //add trandings services
            services
                .AddTradingServices();

        }
    }
}
