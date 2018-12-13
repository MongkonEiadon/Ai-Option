using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Core;
using EventFlow.Queries;

namespace AiOption.Tests
{
    public static class TestCaseSources
    {
        public static UserLevel[] UserLevels =>
            Enum.GetValues(typeof(UserLevel))
                .Cast<UserLevel>()
                .ToArray();
    }
}