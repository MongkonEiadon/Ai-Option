using System;

using EventFlow.Aggregates;

namespace AiOption.Domain.Saga.Events {

    public class RegisterStarted : AggregateEvent<RegisterSaga, RegisterSagaId> {

        public Guid Id { get; }
        public string Email { get; }

        public RegisterStarted(Guid id, string email) {
            Id = id;
            Email = email;

        }

    }

}