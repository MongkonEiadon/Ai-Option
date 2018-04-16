using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using iqoption.domain.Orders;
using iqoption.domain.Users;
using iqoptionapi;
using Microsoft.Extensions.Logging;

namespace iqoption.leaders.app {

    public interface IFollowerManagerService : IClientCollection {
        Task SubscribeFollowerAsync(IObservable<Order> hostsObservable);

    }

    public class FollowerManagerService : IFollowerManagerService {
        private readonly ILogger _logger;

        public List<IIqOptionApi> Clients { get; private set; }

        public FollowerManagerService(ILogger logger) {
            _logger = logger;


            Clients = new List<IIqOptionApi>();
        }

        public Task InitializeClientAsync() {

            var clients = new Follower[] {
                new Follower() {Email = "liie.m@excelbangkok.com", Password = "Code11054"}};

            Clients.AddRange(clients.Select(x => new IqOptionApi(x.Email, x.Password)));
            return Task.WhenAll(Clients.Where(x => !x.IsConnected).Select(x => x.ConnectAsync()));
        }


        public async Task SubscribeFollowerAsync(IObservable<Order> hostsObservable) {

            await InitializeClientAsync();

            hostsObservable
                .Subscribe(x => {

                    var a = x;

                });
            
        }

    }

   
}