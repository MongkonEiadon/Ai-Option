using System;
using Autofac;
using iqoption.trading.services.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace iqoption.trading.services
{
    public static class DependencyModuleExtensions {
        public static IServiceCollection AddTradingServices(this IServiceCollection This) {


            This.AddTransient<IMasterTraderManager, MasterTradersManager>();
            This.AddTransient<IFollowerManager, FollowerManager>();
            This.AddSingleton<TradingPersistenceService>();

            This.AddSingleton<TradingPersistenceServiceMiddleware>();

            return This;
        }


        public static IApplicationBuilder UseTradingServicesMiddleware(this IApplicationBuilder app) {

            app.UseMiddleware<TradingPersistenceServiceMiddleware>();

            return app;
        }

    }

}
