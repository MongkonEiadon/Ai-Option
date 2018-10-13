using System.Collections.Generic;
using System.Linq;
using EventFlow.Snapshots;

namespace AiOption.Domain.Customers.Snapshot
{
    public class CustomerSnapShot : ISnapshot
    {
        public CustomerSnapShot(IEnumerable<CustomerStatus> states)
        {
            States = states ?? Enumerable.Empty<CustomerStatus>();
        }

        public IEnumerable<CustomerStatus> States { get; }
    }

}