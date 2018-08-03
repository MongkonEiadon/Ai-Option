using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using iqoption.domain.Users;
using iqoption.domain.Users.Commands;
using iqoption.domain.Users.Commands.Results;
using Microsoft.AspNetCore.Identity;

namespace iqoption.data.User.CommandHandlers {
    public class ChangeUserRoleCommandHandler : ICommandHandler<UserAggregrate, UserIdentity, ChangeUserRoleCommandResult, ChangeUserRoleCommand> {
        private readonly UserManager<UserDto> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ChangeUserRoleCommandHandler(UserManager<UserDto> userManager, RoleManager<IdentityRole> roleManager) {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ChangeUserRoleCommandResult> ExecuteCommandAsync(UserAggregrate aggregate, ChangeUserRoleCommand command, CancellationToken cancellationToken) {

            var user = await _userManager.FindByIdAsync(command.UserId);
            if (user == null) {
                return new ChangeUserRoleCommandResult(false, $"Can't find user by id :{command.UserId}");
            }

            var currentRole = await _userManager.GetRolesAsync(user);
            if (currentRole != null && currentRole.Any()) {
                await _userManager.RemoveFromRolesAsync(user, currentRole);
            }

            if ((await _userManager.AddToRoleAsync(user, command.RoleName)).Succeeded) {
                return new ChangeUserRoleCommandResult(true, "");
            }

            return new ChangeUserRoleCommandResult(false, "Change role failed!");
        }
    }
}