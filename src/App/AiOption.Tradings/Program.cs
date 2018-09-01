using System;
using System.Threading.Tasks;

using AiOption.Domain.Accounts;
using AiOption.Infrastructure.PersistanceServices;

using AutoMapper;

using EventFlow.MsSql;
using EventFlow.MsSql.EventStores;
using EventFlow.MsSql.SnapshotStores;

using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace AiOption.Tradings {

    public class Program {

        private static void Main(string[] args) {
            //TradingPersistenceService tradingPersistenceService = null;

            try {
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
                var mapper = container.GetService<IMapper>();
                mapper.ConfigurationProvider.AssertConfigurationIsValid();

                ////migrate
                Task.Run(() => {
                    var sql = container.GetService<IMsSqlDatabaseMigrator>();
                    EventFlowEventStoresMsSql.MigrateDatabase(sql);
                    EventFlowSnapshotStoresMsSql.MigrateDatabase(sql);
                });


                var trader = container.GetService<TraderPersistenceService>();
                trader.AppendAccountTask(new Account {
                    EmailAddress = "mongkon.eiadon@gmail.com",
                    Password = "Code11054"
                }).ConfigureAwait(false);


                var follower = container.GetService<FollowerPersistenceService>();
                follower.AppendAccountTask(new Account {
                    EmailAddress = "liie.m@excelbangkok.com",
                    Password = "Code11054"
                }).ConfigureAwait(false);


                while (true) {
                    var input = Console.ReadLine();

                    switch (input) {
                        case "-l": {
                            //tradingPersistenceService.GetListOfSubscribe();
                            break;
                        }
                    }


                }


            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }

    }

}