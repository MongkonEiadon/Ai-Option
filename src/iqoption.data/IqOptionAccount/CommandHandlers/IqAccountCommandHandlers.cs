using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using iqoption.core.data;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Command;
using iqoption.domain.IqOption.Commands;
using Microsoft.Extensions.Logging;

namespace iqoption.data.IqOptionAccount.CommandHandlers {
    public class IqAccountCommandHandlers : 
        ICommandHandler<IqOptionAggregate, IqOptionIdentity, StoreSsidResult, StoreSsidCommand>,
        ICommandHandler<IqOptionAggregate, IqOptionIdentity, DeleteIqAccountResult, DeleteIqAccountCommand> {


        private readonly IRepository<IqOptionAccountDto> _iqAccountRepository;
        private readonly ILogger _logger;

        public IqAccountCommandHandlers(
            
            IRepository<IqOptionAccountDto> iqAccountRepository, ILogger<IqAccountCommandHandlers> logger) {
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

        public async Task<StoreSsidResult> ExecuteCommandAsync(IqOptionAggregate aggregate, StoreSsidCommand command, CancellationToken cancellationToken) {

            var dto = _iqAccountRepository.FirstOrDefault(x =>
                x.IqOptionUserName.ToLower().Equals(command.EmailAddress.ToLower()));

            if (dto != null) {
                dto.Ssid = command.Ssid;
                dto.SsidUpdated = DateTime.Now;
                dto.UpdatedOn = DateTime.Now;

                _iqAccountRepository.Update(dto);

                _logger.LogDebug($"Update ssid for {command.EmailAddress} success!");

                return new StoreSsidResult(true);
            }

            return new StoreSsidResult(false);
        }
    }
}