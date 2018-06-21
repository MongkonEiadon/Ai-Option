using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace iqoptionapi.DependencyInjection
{
    public static class IqOptionExtension {
        public static IServiceCollection AddIqOptionApi(this IServiceCollection services, IqOptionConfiguration configuration) {


            services.Configure<IqOptionConfiguration>(c => { });
            services.AddTransient<IIqOptionApi, IqOptionApi>();


            return services;
        }

      

    }
}