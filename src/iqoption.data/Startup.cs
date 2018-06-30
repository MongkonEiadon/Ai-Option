using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using iqoption.data.DependencyModule;
using iqoption.data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace iqoption.data {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services) {
            //services.AddDbContext<iqOptionContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("iqoptiondb")));


            var builder = new ContainerBuilder();
            builder.RegisterModule<DataAutofacModule>();

            builder.Populate(services);

            var container = builder.Build();

            return container.Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ISeedService seedService) {
            if (env.IsDevelopment()) {
            }


            try {
                seedService.Seed().Wait();
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}