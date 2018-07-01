using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using iqoption.domain.Users;
using iqoption.domain.Users.Commands;
using Microsoft.AspNetCore.Identity;

namespace iqoption.data.User.CommandHandlers {
    public class
        DeleteUserCommandHandler : ICommandHandler<UserAggregrate, UserIdentity, DeleteUserResult, DeleteUserCommand> {
        private readonly UserManager<UserDto> _userManager;

        public DeleteUserCommandHandler(UserManager<UserDto> userManager) {
            _userManager = userManager;
        }

        public async Task<DeleteUserResult> ExecuteCommandAsync(UserAggregrate aggregate, DeleteUserCommand command,
            CancellationToken cancellationToken) {
            var user = await _userManager.FindByNameAsync(command.UserName);


            if (user != null) {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded) return new DeleteUserResult(true);

                return new DeleteUserResult(false, string.Join(",", result.Errors));
            }

            return new DeleteUserResult(false);
        }
    }
}