using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using iqoption.core.Collections;
using iqoption.domain.Users;
using iqoptionapi;
using iqoptionapi.models;
using Microsoft.Extensions.Logging;

namespace iqoption.leaders.app.services
{
    public class IqOptionClientsManager
    {
        private readonly ILogger _logger;
        public ReactiveCollection<HostClient> HostClients { get; }
        public ReactiveCollection<FollowerClient> FollowerClients { get; }

        private Subject<InfoData[]> _infoDataSubject = new Subject<InfoData[]>();
        public IObservable<InfoData[]> HostOpenedPositionStream => _infoDataSubject;

        public IqOptionClientsManager(ILogger logger) {
            _logger = logger;

            HostClients = new ReactiveCollection<HostClient>();
            FollowerClients = new ReactiveCollection<FollowerClient>();
            
            //auto connected to iqoption.com
            HostClients
                .ObserveAdd()
                .Select(x => x.Value.Client)
                    .Merge(FollowerClients.ObserveAdd().Select(x => x.Value.Client))
                .Subscribe(x => { x.ConnectAsync(); });


            //when clients connected get traders list
            HostOpenedPositionStream
                .Select(x => x.FirstOrDefault())
                .Where(x => x.Win.Equals("equal"))
                .Subscribe(x => {
                    _logger.LogTrace($"HOST ({x.UserBalanceId})\topened {x.Direction} {x.ActiveId} with size {x.Sum} {x.Currency}");
                });
        }


        public Task AppendHostUser(string email, string password) {
            var hostClient = new HostClient(email, password);
            if (!HostClients.Contains(hostClient)) {

                //put up host stream
                hostClient
                    .Client
                    .IsConnectedObservable
                    .Where(x => x)
                    .Select(x => hostClient.Client.InfoDatasObservable)
                    .Subscribe(x => 
                        x.Subscribe(infoData => 
                            _infoDataSubject.OnNext(infoData)));

                HostClients.Add(hostClient);
            }
            return Task.CompletedTask;
        }

        public Task AppendFollowerUser(string email, string password) {
            var followerClient = new FollowerClient(email, password, _infoDataSubject);
            if(!FollowerClients.Contains(followerClient))
                FollowerClients.Add(followerClient);


            return Task.CompletedTask;
        }

    }
}
