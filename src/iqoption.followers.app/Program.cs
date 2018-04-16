using System;
using System.IO;
using System.Threading.Tasks;
using iqoption.domain.Common.Configuration;
using iqoption.servicebus.Position;
using iqoptionapi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace iqoption.followers.app
{
    class Program
    {

        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var provider = ConfigureServices(new ServiceCollection());
            var logger = provider.GetService<ILogger<Program>>();

            try
            {
                Console.WriteLine("======================================================");
                Console.WriteLine($"++++++++   Hosting the Follower App start   +++++++++");
                Console.WriteLine("======================================================");

                var app = provider.GetService<Startup>().Run();


                Console.ReadLine();

            }
            catch (Exception ex)
            {
                logger.LogCritical("App Error!", ex);
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
                .AddSingleton<Startup>();

            return services.BuildServiceProvider();
        }

    }
}
