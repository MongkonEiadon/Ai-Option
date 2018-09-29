using System.Data;
using System.Data.SqlClient;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

namespace AiOption.Infrastructure.DataAccess {

    public static class EfConfigurations {

        public static IServiceCollection AddEfConfigurationDomain(this IServiceCollection This,
            IConfiguration config) {

            var constring = config.GetConnectionString("aioptiondb");

            This
                .AddSingleton(config)
                .AddEntityFrameworkSqlServer()
                .AddScoped(c => new AiOptionDbContext())
                .AddTransient<IDbConnection>(c => new SqlConnection(constring))
                .AddDbContext<AiOptionDbContext>(c => {
                    c.UseLoggerFactory(new NullLoggerFactory());
                    c.UseSqlServer(constring);
                   
                });

            This.AddIdentityCore<CustomerDto>();
            This.AddIdentity<CustomerDto, CustomerLevelDto>(identity => {
                    identity.Password.RequireDigit = true;
                    identity.Password.RequireLowercase = false;
                    identity.Password.RequireNonAlphanumeric = false;
                    identity.Password.RequiredLength = 6;
                    identity.Password.RequiredUniqueChars = 0;
                })
                .AddEntityFrameworkStores<AiOptionDbContext>()
                .AddDefaultTokenProviders();


            return This;
        }

    }

}