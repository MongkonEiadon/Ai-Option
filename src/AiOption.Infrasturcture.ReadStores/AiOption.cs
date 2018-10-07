using System.Reflection;
using AiOption.Infrastructure.ReadStores;
using EventFlow;
using EventFlow.Extensions;

// ReSharper disable once CheckNamespace
namespace AiOption
{
    public static class AiOption
    {
        public static Assembly AiOptionAssembly => typeof(ReadStore).Assembly;

        public static IEventFlowOptions AddInfrastructure(this IEventFlowOptions options)
        {
            return options.AddDefaults(AiOptionAssembly);
        }
    }
}

namespace AiOption.Infrastructure.ReadStores
{
    internal static class ReadStore
    {
    }
}