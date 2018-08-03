using System.Collections.Generic;
using Autofac;
using EventFlow;
using EventFlow.Extensions;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Queries;

namespace iqoption.apiservice.DependencyModule {

    public static class ApiServiceEventFlowExtensions {
        public static IEventFlowOptions UseEventFlowOptionsForApiService(this IEventFlowOptions options) {

            options.AddDefaults(typeof(ApiServiceEventFlowExtensions).Assembly);
            return options;
        }
    }
   
}