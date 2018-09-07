using System.Reflection;

using Autofac;

namespace AiOption.Application {

    public class AiAssembly {

        public static Assembly ApplicationAssembly => typeof(AiAssembly).Assembly;

    }


    public class ApplicationModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder) {

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.Name.EndsWith("Services"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }

    }


}