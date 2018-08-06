using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using iqoption.core.data;
using iqoption.data.Services;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Command;
using iqoption.domain.IqOption.Commands;
using Microsoft.Extensions.Logging;

namespace iqoption.data.IqOptionAccount.CommandHandlers {

    public class IqAccountCommandHandlers :
        ICommandHandler<IqOptionAggregate, IqOptionIdentity, StoreSsidResult, StoreSsidCommand>,
        ICommandHandler<IqOptionAggregate, IqOptionIdentity, SetActiveAccountResult, SetActiveAccountcommand>,
        ICommandHandler<IqOptionAggregate, IqOptionIdentity, DeleteIqAccountResult, DeleteIqAccountCommand> {

        private readonly ISqlWrapper _sqlWrapper;
        private readonly IRepository<IqOptionAccountDto> _iqAccountRepository;
        private readonly ILogger _logger;

        public IqAccountCommandHandlers(
            ISqlWrapper sqlWrapper,
            IRepository<IqOptionAccountDto> iqAccountRepository, ILogger<IqAccountCommandHandlers> logger) {
            _sqlWrapper = sqlWrapper;
            _iqAccountRepository = iqAccountRepository;
            _logger = logger;
        }

        public async Task<DeleteIqAccountResult> ExecuteCommandAsync(IqOptionAggregate aggregate,
            DeleteIqAccountCommand command,
            CancellationToken cancellationToken) {
            var dto = await _iqAccountRepository.FirstOrDefaultAsync(x => x.Id == command.IqAccountId);

            if (dto != null) {
                await _iqAccountRepository.DeleteAsync(dto);

                return new DeleteIqAccountResult(true);
            }

            return new DeleteIqAccountResult(false);
        }

        public async Task<StoreSsidResult> ExecuteCommandAsync(IqOptionAggregate aggregate, StoreSsidCommand command,
            CancellationToken cancellationToken) {
            var query = $@"UPDATE IqOptionAccount
                          SET ssid = '{command.Ssid}',
                                    UpdatedOn = getdate(),
                                    SsidUpdated = getdate()
                           WHERE IqOptionAccount.IqOptionUserName = '{command.EmailAddress}'";

            var result = await _sqlWrapper.ExecuteAsync(query);

            return new StoreSsidResult(result > 0);
        }

        public async Task<SetActiveAccountResult> ExecuteCommandAsync(IqOptionAggregate aggregate,
            SetActiveAccountcommand command,
            CancellationToken cancellationToken) {


            var query = $@"UPDATE IqOptionAccount
                           SET 
                            IsActive = @IsActive,
                            UpdatedOn = getdate()
                           WHERE IqOptionAccount.IqOptionUserId = @UserId";

            var result = await _sqlWrapper.ExecuteAsync(query, new {
                IsActive = command.IsActive,
                UserId = command.UserId
            });

            return new SetActiveAccountResult(result == 1);
        }

    }

}