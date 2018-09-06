using System;
using System.Threading;
using System.Threading.Tasks;

using EventFlow.Snapshots;
using EventFlow.Snapshots.Strategies;

namespace AiOption.Domain.Customers {
    
    public class CustomerAggregate : SnapshotAggregateRoot<CustomerAggregate, CustomerId, CustomerAggregateSnapshot> {

        public CustomerAggregate(CustomerId id, ISnapshotStrategy snapshotStrategy) : base(id,
            snapshotStrategy) {
        }

        

        protected override Task<CustomerAggregateSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        protected override Task LoadSnapshotAsync(CustomerAggregateSnapshot aggregateSnapshot, ISnapshotMetadata metadata,
            CancellationToken cancellationToken) {

            throw new NotImplementedException();
        }

    }

}