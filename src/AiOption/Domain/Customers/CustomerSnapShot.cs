using EventFlow.Snapshots;

namespace AiOption.Domain.Customers {
    public class CustomerSnapShot : ISnapshot {
        public CustomerState State { get; }

        public CustomerSnapShot(CustomerState state) {
            State = state;
        }
    }
}