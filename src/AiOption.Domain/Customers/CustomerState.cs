using System;

using AiOption.Domain.Customers.Events;

using EventFlow.Aggregates;
using EventFlow.ReadStores;

using Newtonsoft.Json;

namespace AiOption.Domain.Customers {


    public class CustomerState : AggregateState<CustomerAggregate, CustomerId, CustomerState>,
        IReadModel,
        IAmReadModelFor<CustomerAggregate, CustomerId, RegisterRequested> {

        [JsonProperty("invitation")]
        public string InvitationCode { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("email")]
        public string EmailAddress { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }


        private void Apply(RegisterRequested aggregateEvent) {

            InvitationCode = aggregateEvent.NewCustomer.InvitationCode;
            EmailAddress = aggregateEvent.NewCustomer.EmailAddress;
            Password = aggregateEvent.NewCustomer.Password;
            Id = aggregateEvent.NewCustomer.Id;
        }

        internal void Load(CustomerState load) {
            InvitationCode = load.InvitationCode;
            Id = load.Id;
            Password = load.Password;
            EmailAddress = load.EmailAddress;
            Token = load.Token;
        }

        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, RegisterRequested> domainEvent) {
            Apply(domainEvent.AggregateEvent);
        }

    }
}