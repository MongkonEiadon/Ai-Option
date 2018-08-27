using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using AiOption.Application.Bus;
using AiOption.Domain.Accounts;
using AiOption.Domain.IqOptions.Events;
using AiOption.Infrastructure.Bus.Azure;

using EventFlow;
using EventFlow.Queries;

namespace AiOption.Infrastructure.PersistanceServices
{

    public class FollowerPersistence {

        private readonly IBusReceiver<ActiveAccountQueue, StatusChangeEventItem> _statusChangedBusReceiver;
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;
        private readonly TraderPersistence _traderPersistence;

        public FollowerPersistence(
            IBusReceiver<ActiveAccountQueue, StatusChangeEventItem> statusChangedBusReceiver,
            ICommandBus commandBus,
            IQueryProcessor queryProcessor,
            TraderPersistence traderPersistence
            
            
            ) {
            _statusChangedBusReceiver = statusChangedBusReceiver;
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _traderPersistence = traderPersistence;

            //on active-status change
            _statusChangedBusReceiver?.MessageObservable
                .Subscribe(x => {
                    
                });

        }

        public Task BeginTradings() {

            _traderPersistence.BeginTradings();
            RetreiveAccounts();


            return Task.CompletedTask;
        }


        public Task RetreiveAccounts() {

            return Task.CompletedTask;
        }

        
        public void LoginAsync() {
        }

    }
}
