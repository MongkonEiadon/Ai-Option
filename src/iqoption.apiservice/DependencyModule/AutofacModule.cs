using Autofac;
using iqoption.apiservice.Queries;

namespace iqoption.apiservice.DependencyModule {
    public class ApiServiceModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<LoginCommandHandler>().AsSelf().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<GetProfileCommandHandler>().AsSelf().AsImplementedInterfaces().InstancePerDependency();


            base.Load(builder);
        }
    }
}