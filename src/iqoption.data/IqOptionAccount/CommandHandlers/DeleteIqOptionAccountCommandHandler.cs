using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using iqoption.core.data;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Command;

namespace iqoption.data.IqOptionAccount.CommandHandlers {
    public class DeleteIqOptionAccountCommandHandler : ICommandHandler<IqOptionAggregate, IqOptionIdentity,
        DeleteIqAccountResult, DeleteIqAccountCommand> {
        private readonly IRepository<IqOptionAccountDto> _iqAccountRepository;

        public DeleteIqOptionAccountCommandHandler(IRepository<IqOptionAccountDto> iqAccountRepository) {
            _iqAccountRepository = iqAccountRepository;
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
    }
}