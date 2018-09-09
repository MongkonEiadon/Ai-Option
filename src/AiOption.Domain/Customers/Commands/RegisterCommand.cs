using AiOption.Domain.Common;

using EventFlow.Commands;

namespace AiOption.Domain.Customers.Commands {

    public class RegisterCommand : Command<CustomerAggregate, CustomerId, ResultOf<Customer>> {

        public string UserName { get; }
        public string PasswordHash { get; }


        public RegisterCommand(CustomerId aggregateId, string userName, string passwordHash) : base(aggregateId) {
            UserName = userName;
            PasswordHash = passwordHash;
        }
    }

    

}