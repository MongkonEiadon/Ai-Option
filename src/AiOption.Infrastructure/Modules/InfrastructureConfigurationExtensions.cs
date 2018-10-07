using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AiOption.Infrastructure.Modules
{
    public static class InfrastructureConfigurationExtensions
    {
        public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper();

            return services;
        }
    }
}