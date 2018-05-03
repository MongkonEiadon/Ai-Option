using System;
using System.IO;
using System.Threading.Tasks;
using iqoption.domain.Common.Configuration;
using iqoption.leaders.app.log;
using iqoption.leaders.app.services;
using iqoption.servicebus.Position;
using iqoptionapi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace iqoption.leaders.app
{
    public class Program
    {

        public static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync() {
            var provider = ConfigureServices(new ServiceCollection());
            var logger = provider.GetService<ILogger>();

            try {
                Console.WriteLine("======================================================");
                Console.WriteLine($"++++++++   Hosting the Leaders App start    +++++++++");
                Console.WriteLine("======================================================");

                logger.LogInformation("Logger Startup");

                var app = provider.GetService<Startup>().Run();


                Console.ReadLine();

            }
            catch (Exception ex) {
                logger?.LogCritical($"App Error!", ex);
            }


        }

        private static IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //appsetting complier
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            //logging
            services
                .AddSingleton<ILoggerFactory, LoggerFactory>()
                .AddSingleton<ILogger>(c => c.GetService<ILogger<Startup>>())
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>)) // Add first my already configured instance
                
                .AddLogging(c =>
                    c.SetMinimumLevel(LogLevel.Trace)
                        .AddConsole()
                        .AddConfiguration(configuration.GetSection("Logging")));

            //options-configuration;
            services
                .AddOptions()
                .Configure<IqOptionConfiguration>(o => configuration.GetSection("iqoption").Bind(o))
                .Configure<AzureServiceBusConfiguration>(o => configuration.GetSection("azureservicebus").Bind(o));
            
            //services-container
            services
                .AddSingleton(configuration)
                .AddIqOptionMessageBusService()
                .AddSingleton<Startup>()
                .AddTransient<IqOptionClientsManager>()
                .AddTransient<ITraderManager, TraderManager>();


            var s = services.BuildServiceProvider();

            //configure NLog
            return s;
        }

    }
}
