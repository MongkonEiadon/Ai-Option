using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using iqoption.core.Collections;
using iqoption.core.data;
using iqoption.data.IqOptionAccount;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Command;
using iqoption.domain.IqOption.Commands;
using iqoption.domain.IqOption.Queries;
using iqoption.domain.Positions;
using iqoptionapi.models;
using Microsoft.Extensions.Logging;

namespace iqoption.trading.services.Manager {
    public interface IFollowerManager {
        ConcurrencyReactiveCollection<IqOptionApiClient> Followers { get; }
        Task AppendUser(IqAccount account, IObservable<InfoData> infObservable);
        void RemoveByEmailAddress(string emailAddress);

        Task<List<IqAccount>> GetActiveAccountNotOnFollowersTask();
        Task<List<IqAccount>> GetInActiveAccountNotOnFollowersTask();
    }

    public class FollowerManager : IFollowerManager {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandBus _commandBus;
        private readonly ILogger<FollowerManager> _logger;

        public ConcurrencyReactiveCollection<IqOptionApiClient> Followers { get; }

        public FollowerManager(
            IQueryProcessor queryProcessor,
            ICommandBus commandBus,
            ILogger<FollowerManager> logger) {
            _queryProcessor = queryProcessor;
            _commandBus = commandBus;
            _logger = logger;
            Followers = new ConcurrencyReactiveCollection<IqOptionApiClient>();
        }
        

        public async Task AppendUser(IqAccount account, IObservable<InfoData> openedPositionObservable) {

            var ct = new CancellationToken();
            
            //check if not existing
            if (Followers.All(x => x.Account.IqOptionUserName != account.IqOptionUserName)) {


                var client = new IqOptionApiClient(account);
                var isConnect = await client.Client.OpenSecuredSocketAsync(account.Ssid);
                if (!isConnect) {

                    //if ssid not working -re get ssid
                    var loginResult = await _commandBus.PublishAsync(
                        new IqLoginCommand(IqOptionIdentity.New, account.IqOptionUserName, account.Password), ct);

                    if (!loginResult.IsSuccess) {

                        _logger.LogWarning(new StringBuilder(
                                $"Skipped {account.IqOptionUserName} due can't not loggin {loginResult.Message}")
                            .ToString());
                        client.Dispose();
                        return;
                    }

                    await _commandBus.PublishAsync(
                        new StoreSsidCommand(IqOptionIdentity.New, account.IqOptionUserName, loginResult.Ssid), ct);


                }

                client.SubScribeForTraderStream(openedPositionObservable);


                Followers.Add(client);


                _logger.LogInformation(new StringBuilder($"Add {account.IqOptionUserName},")
                    .AppendLine($"Now trading-followers account = {Followers.Count} Account(s).").ToString());
                
            }
        }

        public void RemoveByEmailAddress(string emailAddress) {
            Followers.Remove(x => x.Account.IqOptionUserName == emailAddress);

            _logger.LogInformation(new StringBuilder($"Remove {emailAddress},")
                    .AppendLine($"Now trading-followers account  = {Followers.Count} Accout(s).")
                    .ToString());
        }


        public Task<List<IqAccount>> GetActiveAccountNotOnFollowersTask() {
            return  _queryProcessor.ProcessAsync(new ActiveAccountQuery(), CancellationToken.None)
                .ContinueWith(
                    t => t.Result.Where(x => !Followers.Select(y => y.Account.IqOptionUserName).Contains(x.IqOptionUserName)).ToList()); ;
        }

        public Task<List<IqAccount>> GetInActiveAccountNotOnFollowersTask() {
            return _queryProcessor.ProcessAsync(new InActiveAccountQuery(), CancellationToken.None)
                .ContinueWith(
                    t => t.Result.Where(x => !Followers.Select(y => y.Account.IqOptionUserName).Contains(x.IqOptionUserName)).ToList());
        }
    }
} 