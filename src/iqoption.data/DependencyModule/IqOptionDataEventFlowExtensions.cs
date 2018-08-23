using System.Collections.Generic;
using System.Reflection;
using EventFlow;
using EventFlow.Extensions;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Queries;

namespace iqoption.data.DependencyModule {
    public static class IqOptionDataEventFlowExtensions {
        public static IEventFlowOptions AddEventFlowForData(this IEventFlowOptions This) {
            
            This.AddDefaults(ThisAssembly);
            return This;
        }

        private static Assembly ThisAssembly { get; } = typeof(IqOptionDataEventFlowExtensions).Assembly;
       
    }
}