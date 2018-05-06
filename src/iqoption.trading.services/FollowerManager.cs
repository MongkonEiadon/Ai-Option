using System;
using System.Linq;
using System.Reactive.Linq;
using iqoption.core.Collections;
using iqoptionapi.models;

namespace iqoption.trading.services
{
    public interface IFollowerManager {
        ConcurrencyReactiveCollection<IqOptionApiClient> Followers { get; }
        void AppendUser(string email, string password, IObservable<InfoData> tradersInfoDataObservable);
    }
    public class FollowerManager : IFollowerManager {
        public ConcurrencyReactiveCollection<IqOptionApiClient> Followers { get; }
        public IObservable<InfoData> TradersInfoDataObservable { get; private set; }


        public FollowerManager() {
            Followers = new ConcurrencyReactiveCollection<IqOptionApiClient>();
        }

        public void AppendUser(string email, string password, IObservable<InfoData> tradersInfoDataObservable) {

            if (Followers.All(x => x.User.Email != email)) {
                var follower = new IqOptionApiClient(email, password, tradersInfoDataObservable.Where(x => x.Win.ToLower() == "equal"));
                
                Followers.Add(follower);
            }

        }

    }
}