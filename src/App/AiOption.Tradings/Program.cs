using System;
using System.Threading.Tasks;

using AiOption.Application.Repositories.ReadOnly;
using AiOption.Infrastructure.DataAccess.Repositories;
using AiOption.Infrastructure.PersistanceServices;

using AutoMapper;

using EventFlow.MsSql;
using EventFlow.MsSql.EventStores;
using EventFlow.MsSql.SnapshotStores;

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

                //Validate mapper
                logger.Debug("Validate Mappers");
                var mapper = container.GetService<IMapper>();
                mapper.ConfigurationProvider.AssertConfigurationIsValid();
                
                ////migrate
                Task.Run(() =>
                {
                    var sql = container.GetService<IMsSqlDatabaseMigrator>();
                    EventFlowEventStoresMsSql.MigrateDatabase(sql);
                    EventFlowSnapshotStoresMsSql.MigrateDatabase(sql);
                });
                
                var follower = container.GetService<FollowerPersistence>();




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