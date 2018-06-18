using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using iqoption.core.Collections;
using iqoption.core.data;
using iqoption.data.Model;
using iqoptionapi.models;
using Microsoft.Extensions.Logging;

namespace iqoption.trading.services
{
    public interface IFollowerManager {
        ConcurrencyReactiveCollection<IqOptionApiClient> Followers { get; }
        void AppendUser(string email, string password, IObservable<InfoData> tradersInfoDataObservable);
        void RemoveByEmailAddress(string emailAddress);

        Task<List<IqOptionAccountDto>> GetActiveAccountNotOnFollowersTask();
        Task<List<IqOptionAccountDto>> GetInActiveAccountOnFollowersTask();
    }

    public class FollowerManager : IFollowerManager
    {
        private readonly IRepository<IqOptionAccountDto> _iqOptionAccountRepository;
        private readonly ILogger _logger;
        public ConcurrencyReactiveCollection<IqOptionApiClient> Followers { get; }
        public IObservable<InfoData> TradersInfoDataObservable { get; private set; }


        public FollowerManager(
            IRepository<IqOptionAccountDto> iqOptionAccountRepository,
            ILogger logger) {
                _iqOptionAccountRepository = iqOptionAccountRepository;
                _logger = logger;
                Followers = new ConcurrencyReactiveCollection<IqOptionApiClient>();
        }

        public void AppendUser(string email, string password, IObservable<InfoData> tradersInfoDataObservable) {

            if (Followers.All(x => x.User.Email != email)) {
                _logger.LogInformation(new StringBuilder($"Add {email},")
                    .AppendLine($"Now trading-followers account = {Followers.Count} Account(s).").ToString());

                var follower = new IqOptionApiClient(email, password, tradersInfoDataObservable.Where(x => x.Win.ToLower() == "equal"));
                Followers.Add(follower);
            }
        }

        public void RemoveByEmailAddress(string emailAddress) {
            var count1 = Followers.Count;
            Followers.Remove(x => x.User.Email == emailAddress);

            _logger
                .LogInformation(new StringBuilder(
                            $"Remove {emailAddress},")
                .AppendLine($"Now trading-followers account  = {Followers.Count} Accout(s).")
                .ToString());

        }


        public Task<List<IqOptionAccountDto>> GetActiveAccountNotOnFollowersTask() {
            return _iqOptionAccountRepository
                .GetAllListAsync(x => x.IsActive && !Followers.Select(y => y.User.Email).Contains(x.IqOptionUserName));
        }

        public Task<List<IqOptionAccountDto>> GetInActiveAccountOnFollowersTask() {
            return _iqOptionAccountRepository
                .GetAllListAsync(x => !x.IsActive && Followers.Select(y => y.User.Email).Contains(x.IqOptionUserName));

        }

    }
}
