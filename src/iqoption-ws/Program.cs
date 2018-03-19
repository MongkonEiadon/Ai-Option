using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Fluent;

namespace iqoption_ws
{
    public class Program
    {
        public static void Main(string[] args) {

            try
            {
             
                BuildWebHost(args).Run();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                
                .UseStartup<Startup>()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    // Setting options.IncludeScopes is required in ASP.NET Core 2.0
                    // apps. Setting IncludeScopes via appsettings configuration files
                    // is a feature that's planned for the ASP.NET Core 2.1 release.
                    // See: https://github.com/aspnet/Logging/pull/706
                    logging.AddConsole(options => options.IncludeScopes = true);
                })

                .UseIISIntegration()
                .Build();
    }
}
