using System;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Customers.DomainServices;
using AiOption.Domain.Customers.Events;

using EventFlow.Snapshots;
using EventFlow.Snapshots.Strategies;

namespace AiOption.Domain.Customers {
    
    public class CustomerAggregate : SnapshotAggregateRoot<CustomerAggregate, CustomerId, CustomerAggregateSnapshot> {


        public CustomerState CustomerState { get; } = new CustomerState();

        public CustomerAggregate(CustomerId id, ISnapshotStrategy snapshotStrategy) : base(id, snapshotStrategy) {
            
            // register customer applier
            Register(CustomerState);
        }


        #region [Emits]

        public void CustomerRegisterRequested(CustomerState newCustomer) => Emit(new CustomerRegisterRequested(newCustomer));


        #endregion



        #region [Snapshots]

        protected override Task<CustomerAggregateSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken) {
            return Task.FromResult(new CustomerAggregateSnapshot(CustomerState));
        }

        protected override Task LoadSnapshotAsync(CustomerAggregateSnapshot aggregateSnapshot, ISnapshotMetadata metadata,
            CancellationToken cancellationToken) {

            CustomerState.Load(aggregateSnapshot.CustomerState);
            return Task.CompletedTask;
        }

        #endregion

    }

}