using System;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Common.Saga;

using EventFlow.Aggregates;
using EventFlow.Sagas;

namespace AiOption.Domain.Customers.Saga {

    public class CustomerSagaLocator : BaseIdSagaLocator
    {

        public CustomerSagaLocator() : base("aggregate_id", id => new CustomerSagaId($"customersaga-{id}")) {
        }

    }

}