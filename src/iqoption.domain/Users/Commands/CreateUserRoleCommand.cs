using EventFlow.Commands;

namespace iqoption.domain.Users.Commands {
    public class CreateUserRoleCommand : Command<UserAggregrate, UserIdentity, UserRoleInfo> {
        public CreateUserRoleCommand(UserIdentity aggregateId, string roleName) : base(aggregateId) {
            RoleName = roleName;
        }

        public string RoleName { get; }
    }
}