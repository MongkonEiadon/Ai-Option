using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Common.Saga;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using AiOption.Domain.Customers.Events;
using AiOption.Domain.Saga.Events;

using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;
using EventFlow.ValueObjects;

namespace AiOption.Domain.Saga {

    public class RegisterSagaId : SingleValueObject<string>, ISagaId {

        public RegisterSagaId(string value) : base(value) { }

    }

    public class RegisterSagaLocator : BaseIdSagaLocator  {
        public RegisterSagaLocator() : base("aggregate_id", id => new RegisterSagaId($"customersaga-{id}")) { }

    }

    public class RegisterSaga : AggregateSaga<RegisterSaga, RegisterSagaId, RegisterSagaLocator>,
        ISagaIsStartedBy<CustomerAggregate, CustomerId, RegisterRequested> {

        public RegisterSaga(RegisterSagaId id) : base(id) { }



        public Task HandleAsync(IDomainEvent<CustomerAggregate, CustomerId, RegisterRequested> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken) {

            Publish(new BeginRegisterCustomerCommand(
                domainEvent.AggregateIdentity, 
                domainEvent.AggregateEvent.NewCustomer.Id,
                domainEvent.AggregateEvent.NewCustomer.EmailAddress,
                domainEvent.AggregateEvent.NewCustomer.InvitationCode));

            return Task.CompletedTask;

           
        }

    }
}
