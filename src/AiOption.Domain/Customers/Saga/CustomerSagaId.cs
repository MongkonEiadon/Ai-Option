using EventFlow.Sagas;
using EventFlow.ValueObjects;

namespace AiOption.Domain.Customers.Saga {

    public class CustomerSagaId : SingleValueObject<string>, ISagaId {

        public CustomerSagaId(string value) : base(value) { }

    }

}