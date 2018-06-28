using Autofac;
using Autofac.Core;
using iqoption.core.data;
using Microsoft.EntityFrameworkCore;

namespace iqoption.data.AutofacModule {
    public class DataAutofacModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<AiOptionContext>().As<DbContext>()
                .Named<DbContext>("iqoptioncontext")
                .InstancePerDependency();


            builder.RegisterType<UnitOfWork<AiOptionContext>>()
                .WithParameters
                (new[] {
                    new ResolvedParameter(
                        (p, ctx) => p.ParameterType == typeof(AiOptionContext),
                        (p, ctx) => ctx.ResolveNamed<DbContext>("iqoptioncontext"))
                })
                .As<IUnitOfWork>();

            builder.RegisterType(typeof(EfCoreRepositoryBase<>)).As<IRepositoryWithDbContext>().InstancePerDependency();
            builder.RegisterType(typeof(EfCoreRepositoryBase<,>)).As<IRepositoryWithDbContext>()
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(EfCoreRepositoryBase<,>))
                .As(typeof(IRepository<,>))
                .WithParameters(new[] {
                    new ResolvedParameter(
                        (p, ctx) => p.ParameterType == typeof(AiOptionContext),
                        (p, ctx) => ctx.ResolveNamed<DbContext>("iqoptioncontext"))
                }).InstancePerDependency();

            builder.RegisterGeneric(typeof(EfCoreRepositoryBase<>)).As(typeof(IRepository<>))
                .WithParameters(new[] {
                    new ResolvedParameter(
                        (p, ctx) => p.ParameterType == typeof(AiOptionContext),
                        (p, ctx) => ctx.ResolveNamed<DbContext>("iqoptioncontext"))
                }).InstancePerDependency();


            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerDependency();


            base.Load(builder);
        }
    }
}