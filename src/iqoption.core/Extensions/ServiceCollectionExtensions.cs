using Microsoft.Extensions.DependencyInjection;
using At = AutoMapper;

namespace iqoption.core.Extensions {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddAutoMapper(this IServiceCollection This) {
            At.ServiceCollectionExtensions.AddAutoMapper(This);


            return This;
        }
    }
}