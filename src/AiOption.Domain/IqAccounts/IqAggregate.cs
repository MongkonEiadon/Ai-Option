using System;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.IqAccounts.Events;

using EventFlow.Core;
using EventFlow.Snapshots;
using EventFlow.Snapshots.Strategies;

namespace AiOption.Domain.IqAccounts {

    #region [Aggregate Component]

    public class IqSnapShot : ISnapshot {

        public IqSnapShot(IqAggregateState iqAggregateState) {
            IqAggregateState = iqAggregateState;
        }

        public IqAggregateState IqAggregateState { get; }

    }


    public class IqIdentity : Identity<IqIdentity> {

        public IqIdentity(string value) : base(value) {
        }

    }

    #endregion


    public class IqAggregate : SnapshotAggregateRoot<IqAggregate, IqIdentity, IqSnapShot> {

        public IqAggregate(IqIdentity id, ISnapshotStrategy snapshotStrategy) : base(id,
            snapshotStrategy ?? SnapshotEveryFewVersionsStrategy.With(100)) {

            Register(IqAggregateState);

        }

        public IqAggregateState IqAggregateState { get; set; } = new IqAggregateState();

        public void LoginFailed(string email, string message) {
            Emit(new IqAccountLoginFailed(email, message));
        }

        protected override Task<IqSnapShot> CreateSnapshotAsync(CancellationToken cancellationToken) {
            return Task.FromResult(new IqSnapShot(IqAggregateState));
        }

        protected override Task LoadSnapshotAsync(IqSnapShot snapshot, ISnapshotMetadata metadata,
            CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

    }

}