using AiOption.Domain.Accounts;

namespace AiOption.Domain.IqAccounts.Events {

    public class IqAccountLoginFailed : EventFlow.Aggregates.IAggregateEvent<IqAggregate, IqIdentity> {

        public string EmailAddress { get; }
        public string Message { get; }

        public IqAccountLoginFailed(string emailAddress, string message) {
            EmailAddress = emailAddress;
            Message = message;

        }

    }

}