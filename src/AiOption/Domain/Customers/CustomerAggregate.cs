using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Accounts;
using AiOption.Domain.Common;
using AiOption.Domain.Customers.Events;
using AiOption.Domain.Customers.Snapshot;
using AiOption.Domain.IqAccounts;
using EventFlow.Aggregates;
using EventFlow.Extensions;
using EventFlow.Sagas;
using EventFlow.Snapshots;
using EventFlow.Snapshots.Strategies;

namespace AiOption.Domain.Customers
{
    [AggregateName("Customer")]
    public class CustomerAggregate : SnapshotAggregateRoot<CustomerAggregate, CustomerId, CustomerSnapShot>
    {
        private readonly CustomerState _state = new CustomerState();

        public CustomerAggregate(CustomerId id) : base(id, SnapshotEveryFewVersionsStrategy.With(10))
        {
            Register(_state);
        }

        #region [Snapshot]

        protected override Task<CustomerSnapShot> CreateSnapshotAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new CustomerSnapShot(_state.Status));
        }

        protected override Task LoadSnapshotAsync(CustomerSnapShot snapshot, ISnapshotMetadata metadata,
            CancellationToken cancellationToken)
        {
            _state.LoadState(snapshot.States);
            return Task.CompletedTask;
        }

        #endregion

        #region Emitters

        public string EmailAddress { get; private set; }

        public void RegisterAnAccount(Email user, Password password, string invitationCode)
        {
            CustomerSpecs.NotCorrectEmailAddress
                .And(Specs.IsNew)
                .ThrowDomainErrorIfNotSatisfied(this);

            Emit(new RequestRegister(user, password, invitationCode));
        }

        public void ChangeLevel(Level level)
        {
            Emit(new RequestChangeLevel(level));
        }

        public void CompletedRegister()
        {
            Emit(new RegisterCompleted());
        }

        public void CreateNewIqAccount(IqAccount iqAccount)
        {
            Specs
                .Exists
                .ThrowDomainErrorIfNotSatisfied(this);

            Emit(new CreateNewIqAccountEvent(iqAccount));
        }

        public void Terminate()
        {
            Specs
                .Exists
                .ThrowDomainErrorIfNotSatisfied(this);

            Emit(new TerminateRequested());
        }

        public void Terminated() => Emit(new TerminateCustomerCompleted());

        public void CreateUserToken()
        {
        }

        #endregion
    }
}