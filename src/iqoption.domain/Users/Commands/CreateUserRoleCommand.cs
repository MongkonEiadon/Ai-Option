using iqoption.core;

namespace iqoption.domain.Users.Commands {
    public class UserRoleInfo {
        public string UserLevel { get; set; }
    }

    public class CreateUserRoleCommand : ICommand<UserRoleInfo> {
        public CreateUserRoleCommand(string level) {
            UserLevel = level;
        }

        public string UserLevel { get; }
    }
}