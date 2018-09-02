using AiOption.Domain;
using AiOption.Domain.Common;

using Autofac;

namespace AiOption.Infrastructure.Modules {

    public class DomainModule : Module {

        protected override void Load(ContainerBuilder builder) {

            builder.RegisterAssemblyTypes(typeof(BaseResult).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

    }

}