﻿using System;
using iqoption.domain.Users;
using iqoptionapi;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using iqoptionapi.models;

namespace iqoption.trading.services {
    public class IqOptionApiClient {
        private readonly IObservable<InfoData> _infoDataObservable;
        public User User { get; }
        public IIqOptionApi ApiClient { get; }

        public IqOptionApiClient(string email, string password, IObservable<InfoData> infoDataObservable = null) {

            _infoDataObservable = infoDataObservable ?? Observable.Empty<InfoData>();
            User = new User() {Email = email, Password = password};
            ApiClient = new IqOptionApi(email, password);


            //auto setup user-id
            ApiClient
                .ProfileObservable
                .Subscribe(x => User.UserId = x.UserId);

            _infoDataObservable
                .Subscribe(async x => {
                    Console.WriteLine($"{User.Email} opened {x.Direction} {x.ActiveId} {x.Direction} {x.Sum} {x.Value}{x.Currency}");
                    var result = await ApiClient.BuyAsync(x.ActiveId, (int) x.Sum, x.Direction, x.Expired);
                    Console.WriteLine(result.ToString());

                });
        }
    }
}