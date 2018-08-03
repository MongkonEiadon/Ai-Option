using EventFlow.Aggregates.ExecutionResults;

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
}