using System;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Accounts;
using AiOption.Domain.API;
using AiOption.Domain.IqAccounts;
using AiOption.Domain.IqAccounts.Commands;
using AiOption.Domain.IqAccounts.Results;

using EventFlow.Commands;

namespace AiOption.Application.CommandHandlers.Accounts {

    public class IqOptionLoginCommandHandler : ICommandHandler<IqAggregate, IqIdentity, LoginCommandResult, LoginCommand> {

        private readonly IIqOptionApiWrapper _apiWrapper;

        public IqOptionLoginCommandHandler(IIqOptionApiWrapper apiWrapper) {
            _apiWrapper = apiWrapper;
        }

        public Task<LoginCommandResult> ExecuteCommandAsync(IqAggregate aggregate, LoginCommand command,
            CancellationToken cancellationToken) {

            var tcs = new TaskCompletionSource<LoginCommandResult>();

            try {
                _apiWrapper.LoginToIqOptionAsync(command.EmailAddress, command.Password)
                    .ContinueWith(t => {

                        if (t.Result.Item1)
                            tcs.TrySetResult(new LoginCommandResult(true, t.Result.Item2));

                        else {
                            aggregate.LoginFailed(command.EmailAddress, t.Result.Item2);

                            tcs.TrySetResult(new LoginCommandResult(false, t.Result.Item2));
                        }

                    }, cancellationToken);
            }
            catch (Exception ex) {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

    }

}