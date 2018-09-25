using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.IqAccounts;
using AiOption.Domain.IqAccounts.Commands;

using EventFlow;

namespace AiOption.Application.Persistences {

    public abstract class ApplicationTradingPersistence {

        private readonly ICommandBus _commandBus;

        public ApplicationTradingPersistence(ICommandBus commandBus) {

            _commandBus = commandBus;

            OpenAccountTradingsStream = new ConcurrentDictionary<Account, IDisposable>();
        }

        protected IDictionary<Account, IDisposable> OpenAccountTradingsStream { get; }


        public virtual Task RemoveAccountTask(Account account) {
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


        public virtual async Task AppendAccountTask(Account account) {
            if (!OpenAccountTradingsStream.ContainsKey(account)) {

                if (string.IsNullOrEmpty(account.SecuredToken)) {
                    var loginResult = await _commandBus.PublishAsync(
                        new LoginCommand(IqIdentity.New, account.EmailAddress, account.Password),
                        CancellationToken.None);

                    if (!loginResult.IsSuccess) return;

                    account.SecuredToken = loginResult.Ssid;
                }

                OpenAccountTradingsStream.Add(account, Handle(account));
            }
        }

        public abstract Task<IDisposable> Handle(Account account);
        public abstract Task<IEnumerable<Account>> GetAccounts();

    }

}