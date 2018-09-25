using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using AiOption.Domain.Customers.DomainServices;

using EventFlow.Aggregates;
using EventFlow.Commands;

namespace AiOption.Application.CommandHandlers.Customers {

    public class BeginRegisterCustomerCommandHandler : ICommandHandler<CustomerAggregate, CustomerId, BaseResult, BeginRegisterCustomerCommand>
    {
        private readonly IAuthorizationDomainService _authorizationDomainService;

        public BeginRegisterCustomerCommandHandler(IAuthorizationDomainService authorizationDomainService)
        {
            _authorizationDomainService = authorizationDomainService;
        }

        public async Task<BaseResult> ExecuteCommandAsync(CustomerAggregate aggregate,
            BeginRegisterCustomerCommand command,
            CancellationToken cancellationToken)
        {

            try
            {
                await _authorizationDomainService.RegisterCustomerAsync(command.NewCustomer);
                aggregate.RegisterSucceeded();
                return new BaseResult(true);
            }
            catch (CustomerException customerException)
            {
                aggregate.RegisterFailed(customerException.Message);
                return new BaseResult(false, customerException.Message);
            }
        }

    }

}