using System.Reflection;
using EventFlow;
using EventFlow.Extensions;
using EventFlow.Snapshots.Strategies;

namespace AiOption
{
    public static class AiOption
    {
        public static Assembly AiOptionAssembly => typeof(AiOption).Assembly;

        public static IEventFlowOptions AddDomain(this IEventFlowOptions options)
        {
            return
                options.AddDefaults(AiOptionAssembly)
                    .RegisterServices(c => c.Register(ct => SnapshotEveryFewVersionsStrategy.With(10)));
        }
    }
}