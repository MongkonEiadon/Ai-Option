using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AiOption.Domain.IqAccounts;
using EventFlow;

namespace AiOption.Application.Persistence
{
    public abstract class ApplicationTradingPersistence
    {
        private readonly ICommandBus _commandBus;

        public ApplicationTradingPersistence(ICommandBus commandBus)
        {
            _commandBus = commandBus;

            OpenAccountTradingsStream = new ConcurrentDictionary<IqAccount, IDisposable>();
        }

        protected IDictionary<IqAccount, IDisposable> OpenAccountTradingsStream { get; }


        public virtual Task RemoveAccountTask(IqAccount account)
        {
            if (OpenAccountTradingsStream.ContainsKey(account))
            {
                var dispose = OpenAccountTradingsStream[account];
                dispose.Dispose();

                OpenAccountTradingsStream.Remove(account);
            }

            return Task.CompletedTask;
        }

        public virtual Task InitialAccount()
        {
            return GetAccounts().ContinueWith(t => Task.WhenAll(t.Result.Select(AppendAccountTask)));
        }


        public virtual async Task AppendAccountTask(IqAccount account)
        {
            if (!OpenAccountTradingsStream.ContainsKey(account))
            {
                if (string.IsNullOrEmpty(account.SecuredToken)) account.SecuredToken = ""; // .SecuredToken = "";

                OpenAccountTradingsStream.Add(account, Handle(account));
            }
        }

        public abstract Task<IDisposable> Handle(IqAccount account);
        public abstract Task<IEnumerable<IqAccount>> GetAccounts();
    }
}