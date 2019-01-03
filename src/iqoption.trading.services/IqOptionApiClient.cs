using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using iqoption.domain.IqOption;
using iqoption.domain.Users;
using IqOptionApi.Models;
using IqOptionApi.ws;
using Microsoft.Extensions.Logging;
using RestSharp.Validation;

namespace iqoption.trading.services {
    public class IqOptionApiClient : IDisposable {
        public IqAccount Account { get; }
        public IqOptionWebSocketClient Client { get; }

        public IqOptionApiClient(IqAccount account) {
            Account = account;
            Client = new IqOptionWebSocketClient(ws =>
            {
                ws.OpenSecuredSocketAsync(account.Ssid);
            });
            
        }

        public Task UpdateSsidTask(string ssid) {
           return Client.OpenSecuredSocketAsync(ssid);
        }

        


        public IDisposable SubScription { get; private set; }

        public void SubScribeForTraderStream(IObservable<InfoData> traderStream)
        {
            SubScription = traderStream.Subscribe(x =>
            {
                var sum = (int) x.Sum;

                switch (Account.Level)
                {
                    case UserLevel.Standard:
                    {
                        sum = sum / 2;
                        break;
                    }
                    case UserLevel.Silver:
                    {
                        sum = sum * 1;
                        break;
                    }
                    case UserLevel.Gold:
                    {
                        sum = sum * 2;
                        break;
                    }
                    case UserLevel.Platinum:
                    {
                        sum = sum * 3;
                        break;
                    }
                    case UserLevel.Vip:
                    {
                        sum = sum * 4;
                        break;
                    }
                }

                if (Account.Level != UserLevel.Standard || Account.Level != UserLevel.None)
                {
                    Console.WriteLine($"========================= User Level = {Account.Level} Order Placed {sum} size ==========================");
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