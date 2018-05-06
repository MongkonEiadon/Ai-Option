using System.IO;
using FluentValidation.AspNetCore;
using iqoption.data;
using iqoption.trading.services;
using iqoption.web.Configurations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace iqoption.web {
    public class Startup {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            //appsetting complier
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .Build();

            //logging
            services
                .AddSingleton<ILoggerFactory, LoggerFactory>()
                .AddSingleton<ILogger>(c => c.GetService<ILogger<Startup>>())
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>)); // Add first my already configured instance


            services
                .AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<iqOptionContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("iqoptiondb")))
                .AddIqOptionIdentity()
                .AddAuthentication()
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

            //add trandings services
            services
                .AddTradingServices();


            //var builder = new ContainerBuilder();


            ////data-modules
            //builder.RegisterModule<DataAutofacModule>();
            //builder.Populate(services);

            //var container = builder.Build();
            //return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            app
                .UseStaticFiles()
                .UseAuthentication()
                .UseCookiePolicy(new CookiePolicyOptions {MinimumSameSitePolicy = SameSiteMode.Strict})
                //.UseTradingServicesMiddleware()
                .UseMvc(routes => {
                    routes.MapRoute(
                        "default",
                        "{controller=Dashboard}/{action=Index}/{id?}");
                });
        }
    }
}