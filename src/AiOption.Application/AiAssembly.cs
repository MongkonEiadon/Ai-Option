using System.Reflection;
using AiOption.Application;
using Autofac;

using Module = Autofac.Module;

namespace AiOption {

    public static partial class AiOption
    {
        public static Assembly ApplicationAssembly => typeof(ApplicationAssembly).Assembly;
    }

    public class ApplicationModule : Module
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

namespace AiOption.Application
{
    static class ApplicationAssembly { }

}