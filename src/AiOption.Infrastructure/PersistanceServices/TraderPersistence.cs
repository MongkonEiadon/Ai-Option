using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

using AiOption.Application.Bus;
using AiOption.Domain.Accounts;
using AiOption.Domain.IqOptions.Events;
using AiOption.Infrastructure.Bus.Azure;

using EventFlow.Queries;

using IqOptionApi.Models;
using IqOptionApi.ws;

namespace AiOption.Infrastructure.PersistanceServices {

    public class TraderPersistence {

        private readonly IQueryProcessor _queryProcessor;
        private readonly IBusReceiver<ActiveTradersQueue, StatusChangeEventItem> _tradersBusReceiver;
        private readonly Subject<InfoData> _tradersOpenPositionSubject = new Subject<InfoData>();

        
        public IDictionary<Account, IDisposable> TraderSocketClients { get; }

        public IObservable<InfoData> TraderOpenPositionStream => _tradersOpenPositionSubject.Publish().RefCount();


        public TraderPersistence(
            IQueryProcessor queryProcessor,
            IBusReceiver<ActiveTradersQueue, StatusChangeEventItem> tradersBusReceiver) {
            
            _queryProcessor = queryProcessor;
            _tradersBusReceiver = tradersBusReceiver;
            TraderSocketClients = new ConcurrentDictionary<Account, IDisposable>();
            _tradersBusReceiver?.MessageObservable
                .Subscribe(x => {

                    //handle new active account

                });

        }

        public Task BeginTradings() {
            

            return Task.CompletedTask;
        }




        public void AddNewClient(Account account) {
            if (!TraderSocketClients.ContainsKey(account)) {

                var client = new IqOptionWebSocketClient(account.SecuredToken.Token);
                var dispose = client.InfoDataObservable
                    .Where(x => x.Any() && x[0].Win == "equal")
                    .Select(x => x[0])
                    .Subscribe(x => { _tradersOpenPositionSubject.OnNext(x); });

                TraderSocketClients.Add(new KeyValuePair<Account, IDisposable>(account, dispose));
            }
        }

        public void RemoveClient(Account account) {
            if (TraderSocketClients.ContainsKey(account)) {
                var client = TraderSocketClients[account];
                client.Dispose();

                //remove
                TraderSocketClients.Remove(account);
            }
        }
    }

}

