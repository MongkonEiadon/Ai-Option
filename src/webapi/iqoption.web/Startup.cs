using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using iqoption.data;
using iqoption.data.AutofacModule;
using iqoption.data.Configurations;
using iqoption.data.Services;
using iqoption.trading.services;
using iqoption.web.AutofacModules;
using iqoption.web.Configurations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace iqoption.web
{
    public partial class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //appsetting complier
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();


            //logging
            services
                .AddSingleton<ILoggerFactory, LoggerFactory>()
                .AddSingleton<ILogger>(c => c.GetService<ILogger<Startup>>())
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>)); // Add first my already configured instance
               
                //     .AddConfiguration(configuration.GetSection("Logging")));

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

            var builder = new ContainerBuilder();


            //data-modules
            builder.RegisterModule<DataAutofacModule>();
            builder.Populate(services);

            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app
                .UseStaticFiles()
                .UseAuthentication()
                .UseCookiePolicy(new CookiePolicyOptions(){MinimumSameSitePolicy = SameSiteMode.Strict})
                .UseTradingServicesMiddleware()
                .UseMvc(routes => {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Dashboard}/{action=Index}/{id?}");
                });
        }

   
    }


    public class TradingPersistanceHostingService : IHostedService {
        
        
       
        
        //
        // Summary:
        //     Triggered when the application host is ready to start the service.
        public Task StartAsync(CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Triggered when the application host is performing a graceful shutdown.
        public Task StopAsync(CancellationToken cancellationToken) {

            return Task.CompletedTask;
        }
    }
}

