using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Mime;
using System;
using AiOption.Application;
using AiOption.Infrastructure.Modules;
using AiOption.Infrastructure.ReadStores;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using EventFlow.AspNetCore.Extensions;
using EventFlow.Autofac.Extensions;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.EntityFramework.Extensions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace AiOption.WebPortal.Server
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
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            // Once you've registered everything in the ServiceCollection, call
            // Populate to bring those registrations into Autofac. This is
            // just like a foreach over the list of things in the collection
            // to add them to Autofac.
            ConfigureContainer(builder);

            //infra-configuration
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                });
            });
            services.AddInfrastructureConfiguration();
            services.AddLogging(c => c.AddConsole());
            services.AddEfConfigurationDomain(Configuration);
            services.AddAutoMapper();

            //event flows
            services.AddEventFlow(cfg =>
                cfg.UseAutofacContainerBuilder(builder)
                    .Configure(c =>
                    {
                        c.IsAsynchronousSubscribersEnabled = true;
                        c.ThrowSubscriberExceptions = true;
                    })
                    .UseServiceCollection(services)
                    .AddDomain()
                    .AddApplication()
                    .AddInfrastructureReadStores()
            );

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            builder.Populate(services);
            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });

            app.UseBlazor<Client.Startup>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var logger = new LoggerConfiguration()

                //.ReadFrom.Configuration(Configuration)
                .CreateLogger();

            builder.RegisterModule(new ConfigurationModule(Configuration));
            builder.RegisterModule<BusModule>();
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<ApplicationModule>();


            builder.Register(c => logger).As<Serilog.ILogger>().SingleInstance();
        }
    }
}
