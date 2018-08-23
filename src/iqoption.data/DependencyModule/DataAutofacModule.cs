using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using Autofac;
using Autofac.Core;
using EventFlow.Configuration;
using iqoption.core.data;
using iqoption.data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Module = Autofac.Module;

namespace iqoption.data.DependencyModule {
    public class DataAutofacModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<AiOptionContext>().As<DbContext>()
                .Named<DbContext>("aioptioncontext")
                .InstancePerLifetimeScope();


            //builder.RegisterType<UnitOfWork<AiOptionContext>>()
            //    .WithParameters
            //    (new[] {
            //        new ResolvedParameter(
            //            (p, ctx) => p.ParameterType == typeof(AiOptionContext),
            //            (p, ctx) => ctx.ResolveNamed<DbContext>("iqoptioncontext"))
            //    })
            //    .As<IUnitOfWork>();

            //builder.RegisterType(typeof(EfCoreRepositoryBase<>)).As<IRepositoryWithDbContext>().SingleInstance();
            //builder.RegisterType(typeof(EfCoreRepositoryBase<,>)).As<IRepositoryWithDbContext>().SingleInstance();


            builder.RegisterGeneric(typeof(EfCoreRepositoryBase<,>))
                .As(typeof(IRepository<,>))
                .WithParameters(new[] {
                    new ResolvedParameter(
                        (p, ctx) => p.ParameterType == typeof(AiOptionContext),
                        (p, ctx) => ctx.ResolveNamed<DbContext>("aioptioncontext"))
                })
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfCoreRepositoryBase<>)).As(typeof(IRepository<>))
                .WithParameters(new[] {
                    new ResolvedParameter(
                        (p, ctx) => p.ParameterType == typeof(AiOptionContext),
                        (p, ctx) => ctx.ResolveNamed<DbContext>("aioptioncontext"))
                })
                .InstancePerLifetimeScope();

            builder.Register(c => new SqlConnection(c.Resolve<IConfigurationRoot>().GetConnectionString("aioptiondb")))
                .As<IDbConnection>().InstancePerDependency();
            builder.RegisterType<SqlWrapper>().As<ISqlWrapper>().InstancePerDependency();

            builder.RegisterGeneric(typeof(QueryReadModel<>)).As(typeof(IQueryReadModel<>)).InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.IsClass && x.FullName.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            

            base.Load(builder);
        }
    }
}