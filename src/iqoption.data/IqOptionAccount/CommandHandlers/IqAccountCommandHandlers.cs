using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using iqoption.bus;
using iqoption.bus.Queues;
using iqoption.core.data;
using iqoption.data.Services;
using iqoption.domain;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Command;
using iqoption.domain.IqOption.Commands;
using Microsoft.Extensions.Logging;

namespace iqoption.data.IqOptionAccount.CommandHandlers {

    public class IqAccountCommandHandlers :
        ICommandHandler<IqAggregate, IqIdentity, CommandResult, StoreSsidCommand>,
        ICommandHandler<IqAggregate, IqIdentity, CommandResult, SetActiveAccountcommand>,
        ICommandHandler<IqAggregate, IqIdentity, CommandResult, DeleteIqAccountCommand> {

        private readonly ISqlWrapper _sqlWrapper;
        private readonly IBusSender<ActiveAccountQueue, SetActiveAccountStatusItem> _activeAccountBusSender;
        private readonly IRepository<IqOptionAccountDto> _iqAccountRepository;
        private readonly ILogger _logger;

        public IqAccountCommandHandlers(
            ISqlWrapper sqlWrapper,
            IBusSender<ActiveAccountQueue, SetActiveAccountStatusItem> activeAccountBusSender,
            IRepository<IqOptionAccountDto> iqAccountRepository, ILogger<IqAccountCommandHandlers> logger) {
            _sqlWrapper = sqlWrapper;
            _activeAccountBusSender = activeAccountBusSender;
            _iqAccountRepository = iqAccountRepository;
            _logger = logger;
        }

        public async Task<CommandResult> ExecuteCommandAsync(IqAggregate aggregate,
            DeleteIqAccountCommand command,
            CancellationToken cancellationToken) {
            var dto = await _iqAccountRepository.FirstOrDefaultAsync(x => x.Id == command.IqAccountId);

            if (dto != null) {
                await _iqAccountRepository.DeleteAsync(dto);

                //publish to bus
                await _activeAccountBusSender.SendAsync(new SetActiveAccountStatusItem(false, dto.IqOptionUserId), cancellationToken);

                return SuccessResult.New;
            }

            return NotSuccessResult.New;
        }

        public async Task<CommandResult> ExecuteCommandAsync(IqAggregate aggregate, 
            StoreSsidCommand command, CancellationToken cancellationToken) {
            var query = $@"UPDATE IqOptionAccount
                          SET ssid = '{command.Ssid}',
                                    UpdatedOn = getdate(),
                                    SsidUpdated = getdate()
                           WHERE IqOptionAccount.IqOptionUserName = '{command.EmailAddress}'";

            var result = await _sqlWrapper.ExecuteAsync(query);

            return new CommandResult(result > 0);
        }

        public async Task<CommandResult> ExecuteCommandAsync(IqAggregate aggregate,
            SetActiveAccountcommand command,
            CancellationToken cancellationToken) {

            var query = $@"UPDATE IqOptionAccount
                           SET 
                            IsActive = @IsActive,
                            UpdatedOn = getdate()
                           WHERE IqOptionAccount.IqOptionUserId = @UserId";

            var result = await _sqlWrapper.ExecuteAsync(query, new {
                IsActive = command.StatusItem.IsActive,
                UserId = command.StatusItem.UserId
            });

            //publish to bus
            await _activeAccountBusSender.SendAsync(command.StatusItem, cancellationToken);

            return new CommandResult(result == 1);
        }

    }

}