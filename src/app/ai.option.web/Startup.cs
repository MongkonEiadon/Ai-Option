using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ai.option.web.Configurations;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using iqoption.data;
using iqoption.data.AutofacModule;
using iqoption.data.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

using iqoption.core.Extensions;
namespace ai.option.web
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
            var builder = new ContainerBuilder();

            //appsetting complier
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            services
                .AddEntityFrameworkInMemoryDatabase()
                //.AddEntityFrameworkSqlServer()
                .AddDbContext<AiOptionContext>(options => options.UseSqlServer(Configuration.GetConnectionString("aioptiondb")))
                .AddIqOptionIdentity()
                .AddAuthentication()
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //logging
            services
                .AddSingleton<ILoggerFactory, LoggerFactory>()
                .AddSingleton<ILogger>(c => c.GetService<ILogger<Startup>>())
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>));


            services
                .AddAutoMapper()
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            builder.RegisterModule<DataAutofacModule>();
            builder.Populate(services);

            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbSeeding seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            seed.SeedAsync().Wait();

            app.UseStaticFiles(new StaticFileOptions()
            {
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), @"Libs")),
            //    RequestPath = new PathString("/libs")
            });

            app.UseHttpsRedirection()
                .UseAuthentication()
                .UseStaticFiles()
                .UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
