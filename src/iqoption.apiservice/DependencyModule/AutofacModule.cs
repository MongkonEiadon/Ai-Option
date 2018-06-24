using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using iqoption.apiservice.Queries;

namespace iqoption.apiservice.DependencyModule
{
    public class ApiServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder) {

            builder.RegisterType<LoginCommandHandler>().AsSelf().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<GetProfileCommandHandler>().AsSelf().AsImplementedInterfaces().InstancePerLifetimeScope();


            base.Load(builder);
        }
    }
}
