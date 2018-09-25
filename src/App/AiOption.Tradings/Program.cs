using System;
using System.Threading;

using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;

using EventFlow;
using EventFlow.Queries;

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


                var bus = container.GetService<ICommandBus>();
                var id = CustomerId.New;
                bus.PublishAsync(new CustomerRegisterCommand(id, new CustomerReadModel {
                    EmailAddress = "m223@email.com",
                    //Password = "Code11054",
                    //InvitationCode = "Invitation"
                }), CancellationToken.None).Wait();


                var query = container.GetService<IQueryProcessor>();
                var resultModel = query.ProcessAsync(new ReadModelByIdQuery<CustomerReadModel>(id), CancellationToken.None)
                    .Result;

                //Validate mapper
                //var mapper = container.GetService<IMapper>();
                //mapper.ConfigurationProvider.AssertConfigurationIsValid();

                //////migrate
                //Task.Run(() => {
                //    var sql = container.GetService<IMsSqlDatabaseMigrator>();
                //    EventFlowEventStoresMsSql.MigrateDatabase(sql);
                //    EventFlowSnapshotStoresMsSql.MigrateDatabase(sql);
                //});


                //var trader = container.GetService<TraderPersistenceService>();
                //var follower = container.GetService<FollowerPersistenceService>();

                //trader.AppendAccountTask(new Account
                //{
                //    EmailAddress = "mongkon.eiadon@gmail.com2",
                //    Password = "Code11054"
                //}).ConfigureAwait(false);


                //follower.AppendAccountTask(new Account
                //{
                //    EmailAddress = "liie.m@excelbangkok.com",
                //    Password = "Code11054"
                //}).ConfigureAwait(false);

                //Task.WhenAll(trader.InitialAccount(), follower.InitialAccount());


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