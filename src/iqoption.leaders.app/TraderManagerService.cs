using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using iqoption.domain.Orders;
using iqoption.domain.Users;
using iqoption.leaders.app.services;
using iqoptionapi;
using Microsoft.Extensions.Logging;
using OrderDirection = iqoptionapi.OrderDirection;

namespace iqoption.leaders.app {
    public interface IClientCollection {
        Task InitializeClientAsync();
    }
    public interface ITraderManager : IClientCollection {
        Task BeginSubscribeTradersTask();
        IObservable<Order> OrderObservable { get; }
    }

    public class TraderManager : ITraderManager {
        private readonly ILogger _logger;
        private readonly IqOptionClientsManager _clientsManager;

        private Subject<Order> _orderSubject;
        public IObservable<Order> OrderObservable { get; }
        

        public TraderManager(ILogger logger, IqOptionClientsManager clientsManager) {
            _logger = logger;
            _clientsManager = clientsManager;

            //pre setup
            _orderSubject  = new Subject<Order>();
            OrderObservable = _orderSubject.Publish();

        }

        public Task InitializeClientAsync() {

            return Task.WhenAll(
                _clientsManager.AppendFollowerUser("mongkon.eiadon@gmail.com", "Code11054"),
                _clientsManager.AppendHostUser("liie.m@excelbangkok.com", "Code11054"));
        }


        public async Task BeginSubscribeTradersTask() {

            await InitializeClientAsync();

        }


    }
}