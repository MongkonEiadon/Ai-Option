using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.IqOptions;
using AiOption.Domain.IqOptions.Commands;
using EventFlow;

namespace AiOption.Application.Persistences {

    public abstract class ApplicationTradingPersistence {

        private readonly ICommandBus _commandBus;

        public ApplicationTradingPersistence(ICommandBus commandBus) {

            _commandBus = commandBus;

            OpenAccountTradingsStream = new ConcurrentDictionary<IqAccount, IDisposable>();
        }

        protected IDictionary<IqAccount, IDisposable> OpenAccountTradingsStream { get; }


        public virtual Task RemoveAccountTask(IqAccount account) {
            if (OpenAccountTradingsStream.ContainsKey(account)) {
                var dispose = OpenAccountTradingsStream[account];
                dispose.Dispose();

                OpenAccountTradingsStream.Remove(account);
            }

            return Task.CompletedTask;
        }

        public virtual Task InitialAccount() {
            return GetAccounts().ContinueWith(t => Task.WhenAll(t.Result.Select(AppendAccountTask)));
        }


        public virtual async Task AppendAccountTask(IqAccount account) {
            if (!OpenAccountTradingsStream.ContainsKey(account)) {

                if (string.IsNullOrEmpty(account.SecuredToken)) {
                    var loginResult = await _commandBus.PublishAsync(
                        new IqAccountLoginCommand(IqId.New, account.UserName.Value, account.Password.Value),
                        CancellationToken.None);

                    if (!loginResult.IsSuccess) return;

                    account.SetSecuredToken("");// .SecuredToken = "";
                }

                OpenAccountTradingsStream.Add(account, Handle(account));
            }
        }

        public abstract Task<IDisposable> Handle(IqAccount account);
        public abstract Task<IEnumerable<IqAccount>> GetAccounts();

    }

}