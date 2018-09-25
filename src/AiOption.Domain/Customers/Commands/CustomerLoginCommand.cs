using EventFlow.Commands;

namespace AiOption.Domain.Customers.Commands {

    public class CustomerLoginCommand : Command<CustomerAggregate, CustomerId> {

        public CustomerLoginCommand(CustomerId aggregateId, string userName, string passwordHash) : base(aggregateId) {
            UserName = userName;
            PasswordHash = passwordHash;
        }

        public string UserName { get; }
        public string PasswordHash { get; }

    }

}