using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iqoption.data;
using iqoption.data.Model;
using iqoption.data.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace iqoption.efdummy
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<iqOptionContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("iqoptiondb")))
                .AddAuthentication()
                
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

            services.AddTransient<ISeedService,  SeedService>();
            services.AddLogging();

            services.AddIdentity<UserDto, IdentityRole>()
                .AddEntityFrameworkStores<iqOptionContext>()
                .AddDefaultTokenProviders();

            
            services.Configure<IdentityOptions>(options => {

                //password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;



            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }

    public static class DatabaseSeedInitializer {
        public static IWebHost Seed(this IWebHost host) {

            using (var scope = host.Services.CreateScope()) {
                var sp = scope.ServiceProvider;
                try {


                    var seedService = sp.GetService<ISeedService>();
                    seedService?.Seed().Wait();
                }
                catch (Exception ex) {
                    var logger = sp.GetService<ILogger>();
                    logger.LogCritical(ex, "An error occurred seeding to db.");
                }
            }

            return host;
        }
    }
}
