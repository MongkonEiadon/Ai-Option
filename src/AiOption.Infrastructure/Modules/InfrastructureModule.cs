using System.Reflection;

using Autofac;

using EventFlow.MsSql;
using EventFlow.MsSql.Extensions;

using Module = Autofac.Module;

namespace AiOption.Infrastructure.Modules {

    public class InfrastructureModule : Module {

        protected override void Load(ContainerBuilder builder) {

            builder.RegisterAssemblyTypes(typeof(InfrastructureModule).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

    }

}