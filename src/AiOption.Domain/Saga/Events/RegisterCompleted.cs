using AiOption.Domain.Customers;

using EventFlow.Aggregates;

namespace AiOption.Domain.Saga.Events {

    public class RegisterCompleted : AggregateEvent<RegisterSaga, RegisterSagaId> {

        public CustomerId Id { get; }
        public string Email { get; }
        public string InvitationCode { get; }

        public RegisterCompleted(CustomerId id, string email, string invitationCode) {
            Id = id;
            Email = email;
            InvitationCode = invitationCode;
        }

    }


}