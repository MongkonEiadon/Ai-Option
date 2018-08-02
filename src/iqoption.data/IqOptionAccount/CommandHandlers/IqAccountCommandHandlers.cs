using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using EventFlow.Commands;
using iqoption.core.data;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Command;
using iqoption.domain.IqOption.Commands;
using Microsoft.Extensions.Logging;

namespace iqoption.data.IqOptionAccount.CommandHandlers {
    public class IqAccountCommandHandlers : 
        ICommandHandler<IqOptionAggregate, IqOptionIdentity, StoreSsidResult, StoreSsidCommand>,
        ICommandHandler<IqOptionAggregate, IqOptionIdentity, SetActiveAccountResult, SetActiveAccountcommand>,
        ICommandHandler<IqOptionAggregate, IqOptionIdentity, DeleteIqAccountResult, DeleteIqAccountCommand> {
        private readonly Func<IDbConnection> _connection;
        private readonly IRepository<IqOptionAccountDto> _iqAccountRepository;
        private readonly ILogger _logger;

        public IqAccountCommandHandlers(
            Func<IDbConnection> connection, 
            IRepository<IqOptionAccountDto> iqAccountRepository, ILogger<IqAccountCommandHandlers> logger) {
            _connection = connection;
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

        public async Task<StoreSsidResult> ExecuteCommandAsync(IqOptionAggregate aggregate, StoreSsidCommand command, CancellationToken cancellationToken)
        {
            var query = $@"UPDATE IqOptionAccount
                          SET ssid = '{command.Ssid}',
                                    UpdatedOn = getdate(),
                                    SsidUpdated = getdate()
                           WHERE IqOptionAccount.IqOptionUserName = '{command.EmailAddress}'";

            var result = await _connection().ExecuteAsync(query);
            return new StoreSsidResult((int)result > 0);
        }

        public async Task<SetActiveAccountResult> ExecuteCommandAsync(IqOptionAggregate aggregate,
            SetActiveAccountcommand command,
            CancellationToken cancellationToken) {

            var query = $@"UPDATE IqOptionAccount
                          SET IsActive = {command.IsActive},
                                    UpdatedOn = getdate()
                           WHERE IqOptionAccount.IqOptionUserId = '{command.UserId}'";

            var result = await _connection().ExecuteAsync(query);
            return new SetActiveAccountResult((int) result == 1);

        }
    }
}