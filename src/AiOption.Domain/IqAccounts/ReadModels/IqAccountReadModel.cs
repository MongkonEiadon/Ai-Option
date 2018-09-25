using AiOption.Domain.IqAccounts.Events;

using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace AiOption.Domain.IqAccounts.ReadModels {

    public class IqAccountReadModel : IReadModel,
        IAmReadModelFor<IqAggregate, IqIdentity, IqAccountLoginFailed> {

        public string EmailAddress { get; private set; }

        public bool IsSuccess { get; private set; }

        public string Message { get; private set; }

        public int Version { get; set; }

        public void Apply(IReadModelContext context,
            IDomainEvent<IqAggregate, IqIdentity, IqAccountLoginFailed> domainEvent) {

            EmailAddress = domainEvent.AggregateEvent.EmailAddress;
            Message = domainEvent.AggregateEvent.Message;
            IsSuccess = false;
        }

    }


}