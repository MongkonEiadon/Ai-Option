using System;
using System.Collections.Generic;
using System.Text;

using AiOption.Application.Bus;
using AiOption.Domain.Accounts;
using AiOption.Domain.Accounts.Events;
using AiOption.Infrastructure.Bus.Azure;

namespace AiOption.Infrastructure.PersistanceServices
{

    public class FollowerPersistance {

        private readonly IBusReceiver<ActiveAccountQueue, StatusChangeEventItem> _statusChangedBusReceiver;

        public FollowerPersistance(IBusReceiver<ActiveAccountQueue, StatusChangeEventItem> statusChangedBusReceiver  ) {
            _statusChangedBusReceiver = statusChangedBusReceiver;



            _statusChangedBusReceiver?.MessageObservable
                .Subscribe(x => {
                    
                });
        }

    }
}
