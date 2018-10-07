using System;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Core;
using EventFlow.Queries;

namespace AiOption.Tests
{
    public class EventFlowTest
    {
        public Task<TResult> Query<TResult>(IQuery<TResult> query)
        {
            throw new NotImplementedException();
        }

        public Task<TResult> Publish<TAggregate, TIdentity, TResult>(ICommand<TAggregate, TIdentity, TResult> command)
            where TResult : IExecutionResult
            where TIdentity : IIdentity
            where TAggregate : IAggregateRoot<TIdentity>
        {
            throw new NotImplementedException();
        }
    }
}