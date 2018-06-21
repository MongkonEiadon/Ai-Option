using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace iqoptionapi {

    internal static class IqOptionLoggerFactory {

        private static ILogger _loggerInstance;

        public static void SetLogger(ILogger logger) {
            _loggerInstance = logger;
        }

        public static ILogger CreateLogger() {

            if (_loggerInstance == null) {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"appsettings.json", optional: true)
                    .Build();


                var loggerFactory = new LoggerFactory()
                    .AddConsole(configuration.GetSection("Logging"))
                    .AddFile("Logs/iqoptionapi--{Date}.txt", minimumLevel: LogLevel.Warning)
                    .AddFile("Logs/iqoptionapi-trace--{Date}.txt", minimumLevel: LogLevel.Trace)
                    .AddDebug(LogLevel.Trace);


                _loggerInstance = loggerFactory.CreateLogger(nameof(IqOptionApi));
            }

            return _loggerInstance;
        }
    }
}