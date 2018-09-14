using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Customers.Commands;
using AiOption.Domain.Customers.Events;
using AiOption.Domain.Customers.Saga.Events;

using EventFlow.Aggregates;
using EventFlow.Sagas;
using EventFlow.Sagas.AggregateSagas;

namespace AiOption.Domain.Customers.Saga {

    public class CustomerSaga : 
        AggregateSaga<CustomerSaga, CustomerSagaId, CustomerSagaLocator>,
        ISagaHandles<CustomerAggregate, CustomerId, CustomerRegisterRequested>,
        IEmit<CustomerRegisterRequestCompleted> {

        private CustomerId CustomerId { get; set; }
        private string Email { get; set; }
        private string InvitationCode { get; set; }


        public CustomerSaga(CustomerSagaId id) : base(id) {
        }


        public void Apply(CustomerRegisterRequestCompleted aggregateEvent) {
            CustomerId = aggregateEvent.Id;
            Email = aggregateEvent.Email;
            InvitationCode = aggregateEvent.InvitationCode;
        }


        public void Apply(CustomerRegisterRequested aggregateEvent)
        {
            Publish(new BeginRegisterCustomerCommand(CustomerId.New, aggregateEvent.NewCustomer.Id, Email, InvitationCode));
        }

        public Task HandleAsync(IDomainEvent<CustomerAggregate, CustomerId, CustomerRegisterRequested> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken) {

            Publish(new BeginRegisterCustomerCommand(CustomerId, domainEvent.AggregateEvent.NewCustomer.Id, Email, InvitationCode));

            return Task.CompletedTask;

        }

    }
}
