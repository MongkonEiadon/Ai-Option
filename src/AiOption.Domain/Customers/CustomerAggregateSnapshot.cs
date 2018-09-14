using EventFlow.Snapshots;

namespace AiOption.Domain.Customers {

    public class CustomerAggregateSnapshot : ISnapshot {

        public CustomerState CustomerState { get; }

        public CustomerAggregateSnapshot(CustomerState customerState) {
            CustomerState = customerState;

        }
    }

}