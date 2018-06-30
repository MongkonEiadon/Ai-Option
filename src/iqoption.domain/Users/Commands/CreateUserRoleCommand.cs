using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Core;
using iqoption.core;

namespace iqoption.domain.Users.Commands {
    public class UserRoleInfo : IExecutionResult {
        public string UserLevel { get; }
        public string RoleId { get;  }
        public bool IsSuccess { get; }

        public UserRoleInfo(string userLevel, string roleId, bool isSuccess) {
            UserLevel = userLevel;
            RoleId = roleId;
            IsSuccess = isSuccess;
        }
    }



    public class CreateUserRoleCommand : Command<UserAggregrate, UserIdentity, UserRoleInfo> {
        public string RoleName { get; }

        public CreateUserRoleCommand(UserIdentity aggregateId, string roleName) : base(aggregateId) {
            RoleName = roleName;
        }
        
    }
}


