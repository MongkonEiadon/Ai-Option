using System;
using System.Text;

using AiOption.Application;
using AiOption.Infrastructure.DataAccess;
using AiOption.Infrastructure.Modules;

using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Serilog;

using Swashbuckle.AspNetCore.Swagger;

namespace AiOption.WebPortal
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

            var container = new ContainerBuilder();
            ConfigureContainer(container);

            services
                .AddCors()
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services
                .AddInfrastructureConfiguration()
                .AddEventFlowInfrastructure(Configuration, container)
                .Configure<AppSettings>(Configuration.GetSection("AppSettings"))
                .AddAuthentication(x => {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x => {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters() {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("AiOptionJwtSecret")),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            container.Populate(services);
            var build = container.Build();

            return new AutofacServiceProvider(build);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseCors(x => { x.AllowAnyOrigin().AllowAnyMethod().AllowCredentials(); })
               .UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "v1/{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {

            var logger = new LoggerConfiguration()
                //.ReadFrom.Configuration(Configuration)
                .WriteTo.Console()
                .CreateLogger();

            builder.RegisterModule(new ConfigurationModule(Configuration));
            builder.RegisterModule<BusModule>();
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<ApplicationModule>();


            builder.Register(c => logger).As<ILogger>().SingleInstance();

        }
    }


}
