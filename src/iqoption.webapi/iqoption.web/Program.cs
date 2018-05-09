using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace iqoption.web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseKestrel(o => {o.Limits.KeepAliveTimeout = TimeSpan.Zero;})
                .UseApplicationInsights()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .ConfigureLogging((ctx, logging) => {
                    logging.AddAzureWebAppDiagnostics();
                    logging.AddEventSourceLogger();
                    logging.AddConsole();
                })
                .Build();
    }
}
