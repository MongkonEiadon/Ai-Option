using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using iqoption.domain.Users;
using iqoptionapi.models;

namespace iqoption.leaders.app.services {
    public class FollowerClient : UserClient<Follower> {
        private readonly IObservable<InfoData[]> _hostObservable;


        public IEnumerable<long> HostUserIds { get; set; } = Enumerable.Empty<long>();

        public FollowerClient(string username, string password, IObservable<InfoData[]> hostObservable) 
            : base(username, password) {


            _hostObservable = hostObservable;
            _hostObservable
                .Where(x => x.Any())
                .Select(x => x[0])
                .Where(x => HostUserIds.Contains(x.UserId) && x.Win == "equal")
                .Subscribe(async x => {
                    Console.WriteLine($"{username} opened {x.Direction} {x.ActiveId} {x.Direction} {x.Sum} {x.Value}{x.Currency}");
                    var result = await Client.BuyAsync(x.ActiveId, (int) x.Sum, x.Direction, x.Expired);
                    Console.WriteLine(result.ToString());
                });
        }
    }
}