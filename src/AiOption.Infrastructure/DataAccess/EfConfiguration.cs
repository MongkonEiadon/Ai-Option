using System.Data;
using System.Data.SqlClient;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

namespace AiOption.Infrastructure.DataAccess {

    public static class EfConfigurations {

        public static IServiceCollection AddEfConfigurationDomain(this IServiceCollection This,
            IConfigurationRoot config) {

            var constring = config.GetConnectionString("aioptiondb");

            This
                .AddSingleton(config)
                .AddEntityFrameworkSqlServer()
                .AddScoped(c => new AiOptionDbContext())
                .AddTransient<IDbConnection>(c => new SqlConnection(constring))

                //.AddDbContextPool<AiOptionDbContext>(c =>
                //    c.UseSqlServer(config.GetConnectionString("aioptiondb")))
                .AddDbContext<AiOptionDbContext>(c => {
                    c.UseLoggerFactory(new NullLoggerFactory());
                    c.UseSqlServer(constring);
                })
                .AddIdentity<CustomerDto, CustomerLevelDto>();


            return This;
        }

    }

}