using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using iqoption.core.data;
using iqoption.data.IqOptionAccount;
using iqoption.domain.Users;
using iqoption.domain.Users.Commands;
using Microsoft.AspNetCore.Identity;

namespace iqoption.data.User.CommandHandlers {
    public class DeleteUserCommandHandler : ICommandHandler<UserAggregrate, UserIdentity, DeleteUserResult, DeleteUserCommand> {
        private readonly UserManager<UserDto> _userManager;
        private readonly IRepository<IqOptionAccountDto, Guid> _iqAccountRepository;

        public DeleteUserCommandHandler(UserManager<UserDto> userManager, IRepository<IqOptionAccountDto, Guid> iqAccountRepository) {
            _userManager = userManager;
            _iqAccountRepository = iqAccountRepository;
        }

        public async Task<DeleteUserResult> ExecuteCommandAsync(UserAggregrate aggregate, DeleteUserCommand command,
            CancellationToken cancellationToken) {
            var user = await _userManager.FindByIdAsync(command.Id);
            

            if (user != null) {
                await _iqAccountRepository.DeleteAsync(x => x.User.Id == user.Id);


                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded) return new DeleteUserResult(true);

                return new DeleteUserResult(false, string.Join(",", result.Errors));
            }

            return new DeleteUserResult(false);
        }
    }
}