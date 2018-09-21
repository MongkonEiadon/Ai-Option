using System;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Common;
using AiOption.Domain.Customers.DomainServices;

using EventFlow.Commands;

namespace AiOption.Domain.Customers.Commands {

    public class BeginRegisterCustomerCommand : Command<CustomerAggregate, CustomerId, BaseResult> {

        public Guid CustomerId { get; }
        public string UserName { get; }
        public string InvitationCode { get; }


        public BeginRegisterCustomerCommand(CustomerId aggregateId, Guid customerId, string userName, string invitationCode) : base(aggregateId) {
            CustomerId = customerId;
            UserName = userName;
            InvitationCode = invitationCode;
        }
    }

    public class BeginRegisterCustomerCommandHandler : ICommandHandler<CustomerAggregate, CustomerId, BaseResult, BeginRegisterCustomerCommand> {

        private readonly IAuthorizationDomainService _authorizationDomainService;

        public BeginRegisterCustomerCommandHandler() {
            _authorizationDomainService = default(IAuthorizationDomainService);
        }

        public async Task<BaseResult> ExecuteCommandAsync(CustomerAggregate aggregate,
            BeginRegisterCustomerCommand command,
            CancellationToken cancellationToken) {

            await _authorizationDomainService.RegisterCustomerAsync(aggregate.CustomerState);

            return new BaseResult(true);
        }

    }

}