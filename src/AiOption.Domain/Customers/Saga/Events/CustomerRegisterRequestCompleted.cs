using System;
using System.Collections.Generic;
using System.Text;

using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Saga.Events
{
    public class CustomerRegisterRequestCompleted  : AggregateEvent<CustomerSaga, CustomerSagaId>
    {

        public CustomerId Id { get; }
        public string Email { get; }
        public string InvitationCode { get; }

        public CustomerRegisterRequestCompleted(CustomerId id, string email, string invitationCode) {
            Id = id;
            Email = email;
            InvitationCode = invitationCode;

        }
    }
}
