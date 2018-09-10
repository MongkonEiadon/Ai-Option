using System;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Customers.DomainServices;
using AiOption.Domain.Customers.Events;

using EventFlow.Snapshots;
using EventFlow.Snapshots.Strategies;

namespace AiOption.Domain.Customers {
    
    public class CustomerAggregate : SnapshotAggregateRoot<CustomerAggregate, CustomerId, CustomerAggregateSnapshot> {

        private readonly ICustomerAuthorizeDomainService _customerAuthorizeDomainService;

        public CustomerAggregate(
            CustomerId id, 
            ISnapshotStrategy snapshotStrategy,
            ICustomerAuthorizeDomainService customerAuthorizeDomainService) : base(id,
            snapshotStrategy) {
            _customerAuthorizeDomainService = customerAuthorizeDomainService;
        }


        public async Task<Customer> RegisterNewCustomerAsync(NewCustomer newCustomer) {
            try {
                var result = await _customerAuthorizeDomainService.RegisterCustomerAsync(newCustomer);


            }
            catch (Exception ex) {

            }


            return result;
        }


        #region [Emits]
        

        #endregion



        protected override Task<CustomerAggregateSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        protected override Task LoadSnapshotAsync(CustomerAggregateSnapshot aggregateSnapshot, ISnapshotMetadata metadata,
            CancellationToken cancellationToken) {

            throw new NotImplementedException();
        }

    }

}