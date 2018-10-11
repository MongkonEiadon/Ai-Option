using EventFlow.Snapshots;

namespace AiOption.Domain.Customers
{
    public class CustomerSnapShot : ISnapshot
    {
        public CustomerSnapShot(CustomerState state)
        {
            State = state;
        }

        public CustomerState State { get; }
    }
}