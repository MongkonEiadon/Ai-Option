using System;
using System.Data;
using System.Data.SqlClient;

using AiOption.Infrastructure.DataAccess;
using AiOption.Infrastructure.Modules;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using AutofacContrib.NSubstitute;

using Microsoft.Extensions.DependencyInjection;

namespace AiOption.Infrastructure.Integration {

    public class BaseSetup : IDisposable {
        public AutoSubstitute AutoSbustitute { get; }

        public BaseSetup() {

            var services = new ServiceCollection();

            services.AddSingleton<AiOptionDbContext>(new AiOptionDbContext());
            services.AddTransient<IDbConnection>(c => new SqlConnection("Server=.\\SQLEXPRESS;Database=aioptiondb_test;Trusted_Connection=True;User ID=sa;Password=sa123"));
            services.AddEntityFrameworkSqlServer();
            services.AddInfrastructureConfiguration();

            AutoSbustitute = new AutoSubstitute(c => {
                c.RegisterModule<DomainModule>();
                c.RegisterModule<InfrastructureModule>();
                c.RegisterModule<BusModule>();
                c.Populate(services);
            });
        }

        public void Dispose() {
            AutoSbustitute.Dispose();
        }

        public TService Resolve<TService>() {
            return AutoSbustitute.Resolve<TService>();
        }

    }

}