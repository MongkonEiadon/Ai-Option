using System;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.API;
using AiOption.Domain.IqOptions;
using AiOption.Domain.IqOptions.Commands;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace AiOption.Application.CommandHandlers.Accounts {

    class
        IqOptionLoginCommandHandler : CommandHandler<IqAggregate, IqId, IqAccountLoginCommand> {

        private readonly IIqOptionApiWrapper _apiWrapper;

        public IqOptionLoginCommandHandler(IIqOptionApiWrapper apiWrapper) {
            _apiWrapper = apiWrapper;
        }

        public Task ExecuteCommandAsync(IqAggregate aggregate, IqAccountLoginCommand command,
            CancellationToken cancellationToken) {

            var tcs = new TaskCompletionSource<IExecutionResult>();

            try {
                //_apiWrapper.LoginToIqOptionAsync(command.EmailAddress, command.Password)
                //    .ContinueWith(t => {

                //        if (t.Result.Item1) {
                //            //tcs.TrySetResult(new LoginCommandResult(true, t.Result.Item2));
                //        }

                //        else {
                //            aggregate.LoginFailed(command.EmailAddress, t.Result.Item2);
                //            //tcs.TrySetResult(new LoginCommandResult(false, t.Result.Item2));
                //        }

                //    }, cancellationToken);
            }
            catch (Exception ex) {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        public override Task ExecuteAsync(IqAggregate aggregate, IqAccountLoginCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}