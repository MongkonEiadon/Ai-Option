using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using iqoption.domain.IqOption;
using iqoption.domain.Users;
using iqoptionapi;
using iqoptionapi.models;
using iqoptionapi.ws;
using Microsoft.Extensions.Logging;
using RestSharp.Validation;

namespace iqoption.trading.services {
    public class IqOptionApiClient : IDisposable {
        public IqAccount Account { get; }
        public IqOptionWebSocketClient Client { get; }

        public IqOptionApiClient([NotNull] IqAccount account) {
            Account = account;
            Client = new IqOptionWebSocketClient();
        }

        public Task UpdateSsidTask(string ssid) {
           return Client.OpenSecuredSocketAsync(ssid);
        }

        


        public IDisposable SubScription { get; private set; }

        public void SubScribeForTraderStream(IObservable<InfoData> traderStream) {
            SubScription = traderStream.Subscribe(x =>
            {
                var sum = (int)x.Sum;
                if (Account.Level == UserLevel.Silver) {
                    sum = sum * 2;
                    Console.WriteLine($"=========================Silver Account *2 Order Placed {sum}==========================");
                }

                Client.BuyAsync(x.ActiveId, sum, x.Direction, x.Expired);
            });
        }

        public void Dispose() {
            SubScription?.Dispose();
            Client?.Dispose();
        }
    }
}