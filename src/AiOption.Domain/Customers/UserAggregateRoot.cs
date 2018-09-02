using System;
using System.Threading;
using System.Threading.Tasks;

using EventFlow.Core;
using EventFlow.Snapshots;
using EventFlow.Snapshots.Strategies;

namespace AiOption.Domain.Customers {

    public class CustomerSnapshot : ISnapshot {

    }


    public class CustomerIdentity : Identity<CustomerIdentity> {

        public CustomerIdentity(string value) : base(value) {
        }

    }


    public class CustomersAggregate : SnapshotAggregateRoot<CustomersAggregate, CustomerIdentity, CustomerSnapshot> {

        public CustomersAggregate(CustomerIdentity id, ISnapshotStrategy snapshotStrategy) : base(id,
            snapshotStrategy) {
        }

        protected override Task<CustomerSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        protected override Task LoadSnapshotAsync(CustomerSnapshot snapshot, ISnapshotMetadata metadata,
            CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

    }

}