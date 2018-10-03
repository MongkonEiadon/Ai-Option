using System;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Application.API;
using AiOption.Domain.Common;
using AiOption.Domain.IqAccounts;
using AiOption.Domain.IqAccounts.Commands;
using EventFlow.Commands;

namespace AiOption.Application.CommandHandlers.Accounts
{
    class VerifyIqAccountCommandHandler : CommandHandler<IqAccountAggregate, IqAccountId, VerifyIqAccountResult,
        VerifyIqAccountCommand>
    {
        private readonly IIqOptionApiWrapper _apiWrapper;

        public VerifyIqAccountCommandHandler(IIqOptionApiWrapper apiWrapper)
        {
            _apiWrapper = apiWrapper;
        }


        public override async Task<VerifyIqAccountResult> ExecuteCommandAsync(IqAccountAggregate aggregate,
            VerifyIqAccountCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _apiWrapper.LoginToIqOptionAsync(command.EmailAddress, command.Password);

            return new VerifyIqAccountResult(result.Item1, result.Item2);
        }
    }
}