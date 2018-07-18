using System;
using System.Diagnostics;
using System.Reactive.Linq;
using iqoption.domain.IqOption;
using iqoption.domain.Users;
using iqoptionapi;
using iqoptionapi.models;
using Microsoft.Extensions.Logging;

namespace iqoption.trading.services {
    public class IqOptionApiClient : IDisposable {
        public IqOptionApiClient(string email, string password) {


            User = new IqOptionUser() {Email = email, Password = password};
            ApiClient = new IqOptionApi(email, password);

            //auto reconnect when time-sync not update
            //Observable.Interval(TimeSpan.FromMinutes(50))
            //    .Where(x => Math.Abs(ApiClient?.WsClient?.TimeSync.Subtract(DateTime.Now).TotalMinutes ?? 0) > 1)
            //    .Subscribe(x => {
            //        _logger.LogWarning("Seem like client disconnect from server try to connect again!");
            //        ApiClient.ConnectAsync();
            //    });


            //auto setup user-id
            ApiClient
                .ProfileObservable
                .Subscribe(x => User.UserId = x.UserId);

           
        }


        public IDisposable SubScription { get; private set; }

        public void SubScribeForTraderStream(IObservable<InfoData> traderStream) {
            SubScription = traderStream.Subscribe(x => {

                this.ApiClient.BuyAsync(x.ActiveId, (int) x.Sum, x.Direction, x.Expired);
            });
        }

        public IqOptionUser User { get; }

        public IIqOptionApi ApiClient { get; }

        private ILogger _logger => new LoggerFactory().CreateLogger(nameof(IqOptionApi));

        public void Dispose() {
            SubScription?.Dispose();
            ApiClient?.Dispose();
        }
    }
}