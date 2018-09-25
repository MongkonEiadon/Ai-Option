using EventFlow.Snapshots;

namespace AiOption.Domain.Customers {

    public class CustomerAggregateSnapshot : ISnapshot {

        public CustomerAggregateSnapshot(CustomerReadModel customerReadModel) {
            CustomerReadModel = customerReadModel;

        }

        public CustomerReadModel CustomerReadModel { get; }

    }

}