using AiOption.Domain;

using Autofac;

namespace AiOption.Infrastructure.Modules {

    public class DomainModule : Module {

        protected override void Load(ContainerBuilder builder) {

            //builder.RegisterAssemblyTypes(typeof(BaseResult).Assembly)
            //    .AsImplementedInterfaces()
            //    .InstancePerLifetimeScope();
        }

    }

}