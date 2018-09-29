using EventFlow.Core;

namespace AiOption.Domain.Customers {

    public class CustomerIdentity : Identity<CustomerIdentity> {

        public CustomerIdentity(string value) : base(value) {
        }

    }

}