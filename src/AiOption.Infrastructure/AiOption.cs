using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AiOption.Infrastructure;
using EventFlow;
using EventFlow.Extensions;

// ReSharper disable once CheckNamespace
namespace AiOption
{
    public static partial class AiOption
    {
        public static Assembly AiOptionAssembly => typeof(Infrasturcture).Assembly;

        public static IEventFlowOptions AddEventflowDefaultsForInfrastructure(this IEventFlowOptions options)
        {
            return options.AddDefaults(AiOptionAssembly);
        }
    }

}

namespace AiOption.Infrastructure  {
    static class Infrasturcture { }
}
