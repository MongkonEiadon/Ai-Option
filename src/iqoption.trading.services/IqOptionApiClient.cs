using System;
using System.Reactive.Linq;
using iqoption.domain.Users;
using iqoptionapi;
using iqoptionapi.models;
using Microsoft.Extensions.Logging;

namespace iqoption.trading.services {
    public class IqOptionApiClient : IDisposable
    {
        private readonly IObservable<InfoData> _infoDataObservable;

        public User User { get; }

        public IIqOptionApi ApiClient { get; }

        private ILogger _logger => new LoggerFactory().CreateLogger(nameof(IqOptionApi));

        public IqOptionApiClient(string email, string password, IObservable<InfoData> infoDataObservable = null) {

            _infoDataObservable = infoDataObservable ?? Observable.Empty<InfoData>();


            User = new User() { Email = email, Password = password };
            ApiClient = new IqOptionApi(email, password);

            //auto reconnect when time-sync not update
            Observable.Interval(TimeSpan.FromMinutes(1))
                .Where(x => Math.Abs(ApiClient?.WsClient?.TimeSync.Subtract(DateTime.Now).TotalMinutes ?? 0) > 1)
                .Subscribe(x => {
                    _logger.LogWarning("Seem like client disconnect from server try to connect again!");
                    ApiClient.ConnectAsync();
                });
                
            

            //auto setup user-id
            ApiClient
                .ProfileObservable
                .Subscribe(x => User.UserId = x.UserId);

            _infoDataObservable
                .Subscribe(async x => {
                    var result = await ApiClient.BuyAsync(x.ActiveId, (int)x.Sum, x.Direction, x.Expired);
                });
        }

        public void Dispose() {
            ApiClient?.Dispose();
        }
    }
}