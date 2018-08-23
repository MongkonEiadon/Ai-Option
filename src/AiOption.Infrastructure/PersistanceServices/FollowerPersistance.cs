using System;
using System.Collections.Generic;
using System.Text;

using AiOption.Application.Bus;
using AiOption.Domain.Accounts;
using AiOption.Domain.Accounts.Events;
using AiOption.Infrastructure.Bus.Azure;

using EventFlow;
using EventFlow.Queries;

namespace AiOption.Infrastructure.PersistanceServices
{

    public class FollowerPersistance {

        private readonly IBusReceiver<ActiveAccountQueue, StatusChangeEventItem> _statusChangedBusReceiver;
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public FollowerPersistance(IBusReceiver<ActiveAccountQueue, StatusChangeEventItem> statusChangedBusReceiver ,
            ICommandBus commandBus,
            IQueryProcessor queryProcessor
            
            
            ) {
            _statusChangedBusReceiver = statusChangedBusReceiver;
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;


            _statusChangedBusReceiver?.MessageObservable
                .Subscribe(x => {
                    
                });
        }

        
        public void LoginAsync() {
        }

    }
}
