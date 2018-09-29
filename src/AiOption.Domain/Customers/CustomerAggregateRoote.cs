using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Customers.Commands;
using AiOption.Domain.Customers.Events;

using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Extensions;
using EventFlow.Snapshots;
using EventFlow.Snapshots.Strategies;

namespace AiOption.Domain.Customers {

    public class CustomerAggregateRoote : AggregateRoot<CustomerAggregateRoote, CustomerIdentity> //, CustomerAggregateSnapshot> {
    {

        private readonly ICommandBus _commandBus;
        private readonly CustomerReadModel _customerReadModel = new CustomerReadModel();

        public CustomerAggregateRoote(CustomerIdentity identity, ICommandBus commandBus) : base(identity)
        {
            _commandBus = commandBus;

        }

        //public CustomerAggregateRoote(CustomerIdentity identity, ISnapshotStrategy snapshotStrategy, ICommandBus commandBus) : base(identity,
        //    snapshotStrategy) {
        //    _commandBus = commandBus;

        //    // register customer applier
        //    //Register(_customerReadModel);
        //    Register<CustomerRegisterFailed>(e => _customerReadModel.Apply(this, e));
        //    Register<CustomerRegisterSucceeded>(e => _customerReadModel.Apply(this, e));
        //    Register<RegisterRequested>(e =>
        //    {
        //        _customerReadModel.Apply(this, e);
        //    });
        //}

        //#region [Snapshots]

        //protected override Task<CustomerAggregateSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
        //{
        //    return Task.FromResult(new CustomerAggregateSnapshot(_customerReadModel));
        //}

        //protected override Task LoadSnapshotAsync(CustomerAggregateSnapshot aggregateSnapshot,
        //    ISnapshotMetadata metadata,
        //    CancellationToken cancellationToken)
        //{

        //    _customerReadModel.Load(aggregateSnapshot.CustomerReadModel);

        //    return Task.CompletedTask;
        //}

        //#endregion





        #region [Emits]

        public void CustomerRegisterRequested(CustomerReadModel newCustomer) {
            Emit(new RegisterRequested(newCustomer));

            //_commandBus.Publish(new BeginRegisterCustomerCommand(Id, newCustomer));
        }

        public void RegisterFailed(string failedMessage) {
            Emit(new CustomerRegisterFailed(failedMessage));
        }

        public void RegisterSucceeded() {
            Emit(new CustomerRegisterSucceeded(_customerReadModel));
        }

        #endregion

      
    }

}