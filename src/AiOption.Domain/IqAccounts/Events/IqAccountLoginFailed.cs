using EventFlow.Aggregates;

namespace AiOption.Domain.IqAccounts.Events {

    public class IqAccountLoginFailed : IAggregateEvent<IqAggregate, IqIdentity> {

        public IqAccountLoginFailed(string emailAddress, string message) {
            EmailAddress = emailAddress;
            Message = message;

        }

        public string EmailAddress { get; }
        public string Message { get; }

    }

}