using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using iqoption.core.Collections;
using iqoption.core.data;
using iqoption.data.IqOptionAccount;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Queries;
using iqoptionapi.models;
using Microsoft.Extensions.Logging;

namespace iqoption.trading.services.Manager {
    public interface IFollowerManager {
        ConcurrencyReactiveCollection<IqOptionApiClient> Followers { get; }
        void AppendUser(string email, string password);
        void RemoveByEmailAddress(string emailAddress);

        Task<List<IqAccount>> GetActiveAccountNotOnFollowersTask();
        Task<List<IqAccount>> GetInActiveAccountNotOnFollowersTask();
    }

    public class FollowerManager : IFollowerManager {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ILogger _logger;


        public FollowerManager(
            IQueryProcessor queryProcessor,
            ILogger logger) {
            _queryProcessor = queryProcessor;
            _logger = logger;
            Followers = new ConcurrencyReactiveCollection<IqOptionApiClient>();
        }
        
        public ConcurrencyReactiveCollection<IqOptionApiClient> Followers { get; }

        public void AppendUser(string email, string password) {
            if (Followers.All(x => x.User.Email != email)) {
                _logger.LogInformation(new StringBuilder($"Add {email},")
                    .AppendLine($"Now trading-followers account = {Followers.Count} Account(s).").ToString());

                var follower = new IqOptionApiClient(email, password);
                Followers.Add(follower);
            }
        }

        public void RemoveByEmailAddress(string emailAddress) {
            Followers.Remove(x => x.User.Email == emailAddress);

            _logger.LogInformation(new StringBuilder($"Remove {emailAddress},")
                    .AppendLine($"Now trading-followers account  = {Followers.Count} Accout(s).")
                    .ToString());
        }


        public Task<List<IqAccount>> GetActiveAccountNotOnFollowersTask() {
            return  _queryProcessor.ProcessAsync(new ActiveAccountQuery(), CancellationToken.None)
                .ContinueWith(
                    t => t.Result.Where(x => !Followers.Select(y => y.User.Email).Contains(x.IqOptionUserName)).ToList()); ;
        }

        public Task<List<IqAccount>> GetInActiveAccountNotOnFollowersTask() {
            return _queryProcessor.ProcessAsync(new InActiveAccountQuery(), CancellationToken.None)
                .ContinueWith(
                    t => t.Result.Where(x => !Followers.Select(y => y.User.Email).Contains(x.IqOptionUserName)).ToList());
        }
    }
} 