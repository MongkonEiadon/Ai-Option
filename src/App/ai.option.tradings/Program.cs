using System;
using System.Linq;
using System.Threading.Tasks;
using EventFlow.MsSql;
using EventFlow.MsSql.EventStores;
using EventFlow.MsSql.SnapshotStores;
using iqoption.data;
using iqoption.data.IqOptionAccount;
using iqoption.trading.services;
using Microsoft.Extensions.DependencyInjection;

namespace ai.option.tradings
{
    public class Program
    {
        private static void Main(string[] args)
        {
            TradingPersistenceService tradingPersistenceService = null;

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
                var startup = new Startup();
                var provider = startup.ConfigureServices(services);

                
                //migrate
                //Task.Run(() => {
                //    var sql = provider.GetService<IMsSqlDatabaseMigrator>();
                //    EventFlowEventStoresMsSql.MigrateDatabase(sql);
                //    EventFlowSnapshotStoresMsSql.MigrateDatabase(sql);
                //});


                tradingPersistenceService = provider.GetService<TradingPersistenceService>();
                tradingPersistenceService.InitializeTradingsServiceAsync().Wait();


                while (true)
                {
                    var input = Console.ReadLine();
                    switch (input) {
                        case "-l": {
                            tradingPersistenceService.GetListOfSubscribe();
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
