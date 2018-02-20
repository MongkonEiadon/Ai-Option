using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using iqoption.core.data;
using iqoption.data.Model;
using Microsoft.EntityFrameworkCore;

namespace iqoption.data.AutofacModule
{
    public class DataAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<iqOptionContext>().As<DbContext>().Named<DbContext>("iqoptioncontext").SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<UnitOfWork<iqOptionContext>>()
                .WithParameters
                (new []{
                    new ResolvedParameter(
                               (p,ctx) => p.ParameterType == typeof(iqOptionContext),
                               (p,ctx) => ctx.ResolveNamed<DbContext>("iqoptioncontext"))
                })
                .As<IUnitOfWork>();

            builder.RegisterType<EfCoreRepositoryBase<Person>>()
                .As<IRepository<Person>>()
                .WithParameters(new[]
                {
                    new ResolvedParameter(
                        (p, ctx) => p.ParameterType == typeof(iqOptionContext),
                        (p, ctx) => ctx.ResolveNamed<DbContext>("iqoptioncontext"))
                });


            base.Load(builder);
        }
    }
}
