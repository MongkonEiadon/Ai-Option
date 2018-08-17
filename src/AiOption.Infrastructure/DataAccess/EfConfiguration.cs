using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

namespace AiOption.Infrastructure.DataAccess
{
    public static class EfConfigurations
    {

        public static IServiceCollection AddEfConfigurationDomain(this IServiceCollection This, IConfigurationRoot config) {

            This
                .AddSingleton<IConfigurationRoot>(config)
                .AddEntityFrameworkSqlServer()
                //.AddDbContextPool<AiOptionDbContext>(c => 
                //    c.UseSqlServer(config.GetConnectionString("aioptiondb")))
                .AddDbContext<AiOptionDbContext>(c =>
                {
                    c.UseLoggerFactory(new NullLoggerFactory());
                    c.UseSqlServer(config.GetConnectionString("aioptiondb"));
                })
                .AddIdentity<CustomerDto, CustomerLevelDto>();


            return This;
        }
    }
}
