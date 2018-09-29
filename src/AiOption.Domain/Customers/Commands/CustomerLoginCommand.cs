using EventFlow.Commands;

namespace AiOption.Domain.Customers.Commands {

    public class CustomerLoginCommand : Command<CustomerAggregateRoote, CustomerIdentity> {

        public CustomerLoginCommand(CustomerIdentity aggregateIdentity, string userName, string passwordHash) : base(aggregateIdentity) {
            UserName = userName;
            PasswordHash = passwordHash;
        }

        public string UserName { get; }
        public string PasswordHash { get; }

    }

}