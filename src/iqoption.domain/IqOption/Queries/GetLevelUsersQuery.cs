using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Core;
using iqoption.domain.Users;

namespace iqoption.domain.IqOption.Queries
{
    public class GetLevelUsersQueryResult : IExecutionResult
    {
        public bool IsSuccess { get; }
        public List<IqAccount> Accounts { get; }
        public UserLevel Level { get; }
        public List<IqAccount> IqAccounts { get; }

        public GetLevelUsersQueryResult(bool isSuccess, List<IqAccount> accounts, UserLevel level) {
            IsSuccess = isSuccess;
            Accounts = accounts;
            Level = level;
        }
    }

    public class GetLevelUsersQuery : Command<IqOptionAggregate, IqOptionIdentity, GetLevelUsersQueryResult>
    {
        public UserLevel Level { get; }

        public GetLevelUsersQuery(IqOptionIdentity aggregateId, UserLevel level) : base(aggregateId) {
            Level = level;
        }
        
    }
}
