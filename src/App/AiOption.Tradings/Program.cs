using System;

using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace AiOption.Tradings {

    public class Program
    {
        private static void Main(string[] args)
        {
            //TradingPersistenceService tradingPersistenceService = null;

            try
            {
                Console.WriteLine(@"
                                     █████╗ ██╗       ██████╗ ██████╗ ████████╗██╗ ██████╗ ███╗   ██╗
                                    ██╔══██╗██║      ██╔═══██╗██╔══██╗╚══██╔══╝██║██╔═══██╗████╗  ██║
                                    ███████║██║█████╗██║   ██║██████╔╝   ██║   ██║██║   ██║██╔██╗ ██║
                                    ██╔══██║██║╚════╝██║   ██║██╔═══╝    ██║   ██║██║   ██║██║╚██╗██║
                                    ██║  ██║██║      ╚██████╔╝██║        ██║   ██║╚██████╔╝██║ ╚████║
                                    ╚═╝  ╚═╝╚═╝       ╚═════╝ ╚═╝        ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝");
                var services = new ServiceCollection();
                var container = new Startup().ConfigureServices();
                var logger = container.GetService<ILogger>();

                logger.Debug("Debug");
                logger.Verbose("Verbose");
                logger.Information("info");
                logger.Error("Error");
                logger.Warning("Warning");



                //var provider = startup.ConfigureServices(services);


                ////migrate
                //Task.Run(() =>
                //{
                //    var sql = provider.GetService<IMsSqlDatabaseMigrator>();
                //    EventFlowEventStoresMsSql.MigrateDatabase(sql);
                //    EventFlowSnapshotStoresMsSql.MigrateDatabase(sql);
                //});


                //tradingPersistenceService = provider.GetService<TradingPersistenceService>();
                //tradingPersistenceService.InitializeTradingsServiceAsync().Wait();


                while (true)
                {
                    var input = Console.ReadLine();
                    switch (input)
                    {
                        case "-l":
                        {
                            //tradingPersistenceService.GetListOfSubscribe();
                            break;
                        }
                    }


                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }
    }

}