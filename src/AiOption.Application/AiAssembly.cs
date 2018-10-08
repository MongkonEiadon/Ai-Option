using System.Reflection;
using AiOption.Application;
using Autofac;
using EventFlow;
using EventFlow.Extensions;
using Module = Autofac.Module;

namespace AiOption.Application
{
    public static class AiOptionApplication
    {
        public static Assembly ApplicationAssembly => typeof(ApplicationAssembly).Assembly;

        public static IEventFlowOptions AddApplication(this IEventFlowOptions options)
        {
            return options.AddDefaults(ApplicationAssembly);
        }
    }

    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }

    internal static class ApplicationAssembly
    {
    }
}