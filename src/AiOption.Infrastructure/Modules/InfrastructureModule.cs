using System.Reflection;

using AiOption.Infrastructure.DataAccess;

using Autofac;

using EventFlow.MsSql;
using EventFlow.MsSql.Extensions;

using Microsoft.EntityFrameworkCore;

using Module = Autofac.Module;

namespace AiOption.Infrastructure.Modules {

    public class InfrastructureModule : Module {

        protected override void Load(ContainerBuilder builder) {

            builder.RegisterAssemblyTypes(typeof(InfrastructureModule).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();


            builder.RegisterType<AiOptionDbContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();
        }

    }

}