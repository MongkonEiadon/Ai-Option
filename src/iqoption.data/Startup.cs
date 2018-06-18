using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using iqoption.core.data;
using iqoption.data;
using iqoption.data.AutofacModule;
using iqoption.data.Model;
using iqoption.data.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace iqoption.data
{
    
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
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
