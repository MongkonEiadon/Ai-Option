using System;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Accounts;
using AiOption.Domain.Accounts.Commands;
using AiOption.Domain.Accounts.Results;
using AiOption.Domain.API;

using EventFlow.Commands;

namespace AiOption.Application.CommandHandlers.Accounts {

    public class
        IqOptionLoginCommandHandler : ICommandHandler<IqAggregateRoot, IqIdentity, LoginCommandResult, LoginCommand> {

        private readonly IIqOptionApiWrapper _apiWrapper;

        public IqOptionLoginCommandHandler(IIqOptionApiWrapper apiWrapper) {
            _apiWrapper = apiWrapper;
        }

        public Task<LoginCommandResult> ExecuteCommandAsync(IqAggregateRoot aggregate, LoginCommand command,
            CancellationToken cancellationToken) {

            var tcs = new TaskCompletionSource<LoginCommandResult>();

            try {
                _apiWrapper.LoginToIqOptionAsync(command.EmailAddress, command.Password)
                    .ContinueWith(t => {

                        if (t.Result.Item1) tcs.TrySetResult(new LoginCommandResult(true, t.Result.Item2));

                        tcs.TrySetResult(new LoginCommandResult(false, t.Result.Item2));

                    }, cancellationToken);
            }
            catch (Exception ex) {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

    }

}