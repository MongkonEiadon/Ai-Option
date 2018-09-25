using System.Collections.Generic;

using AiOption.Domain.IqAccounts.Events;

using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace AiOption.Domain.IqAccounts.ReadModels {

    public class IqAccountReadModelLocator : IReadModelLocator {

        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent) {

            var readModel = domainEvent as IqAccountLoginFailed;

            if (readModel != null) yield break;

            yield return readModel.EmailAddress;
        }

    }

}