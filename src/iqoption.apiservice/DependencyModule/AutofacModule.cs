using System.Collections.Generic;
using Autofac;
using EventFlow.Extensions;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Queries;

namespace iqoption.apiservice.DependencyModule {
    public class ApiServiceModule : Module {
        protected override void Load(ContainerBuilder builder) {

            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsImplementedInterfaces()
                .InstancePerDependency();


            base.Load(builder);
        }
    }
}