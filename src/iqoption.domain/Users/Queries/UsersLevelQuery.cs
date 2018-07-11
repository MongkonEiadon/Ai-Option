using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Queries;

namespace iqoption.domain.Users.Queries
{
    public class UsersLevelQueryResult : IExecutionResult {
        public bool IsSuccess { get; }
        public string UserLevel { get; }
        public List<User> Users { get; }

        public UsersLevelQueryResult(List<User> users, string userLevel, bool isSuccess = true) {
            Users = users;
            UserLevel = userLevel;
            IsSuccess = isSuccess;
        }

    }
    public class UsersLevelQuery : IQuery<UsersLevelQueryResult>
    {
        public string UserLevel { get; }

        public UsersLevelQuery(string userLevel = "") {
            UserLevel = userLevel;
        }
    }
}
