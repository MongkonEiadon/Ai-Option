using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Core;
using EventFlow.Snapshots;
using EventFlow.Snapshots.Strategies;

namespace AiOption.Domain.IqOption
{

    #region [Aggregate Component]
    public class IqSnapShot : ISnapshot
    {

    }
    public class IqIdentity : Identity<IqIdentity>
    {
        public IqIdentity(string value) : base(value)
        {
        }
    }
    #endregion

    public class IqAggregateRoot : SnapshotAggregateRoot<IqAggregateRoot, IqIdentity, IqSnapShot>
    {
        public IqAggregateRoot(IqIdentity id, ISnapshotStrategy snapshotStrategy) : base(id, snapshotStrategy)
        {
        }

        protected override Task<IqSnapShot> CreateSnapshotAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task LoadSnapshotAsync(IqSnapShot snapshot, ISnapshotMetadata metadata, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
