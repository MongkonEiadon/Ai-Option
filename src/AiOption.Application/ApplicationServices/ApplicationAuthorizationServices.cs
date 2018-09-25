using System.Threading;
using System.Threading.Tasks;

using AiOption.Application.Repositories.ReadOnly;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using AiOption.Domain.Customers.Queries;

using EventFlow;
using EventFlow.Queries;

namespace AiOption.Application.ApplicationServices {

    public interface IApplicationAuthorizationServices {

        Task<CustomerReadModel> RegisterCustomerAsync(string userName, string password, string invitationCode);

        Task<CustomerReadModel> LoginAsync(string userName, string password);

    }


    public class ApplicationAuthorizationServices : IApplicationAuthorizationServices {

        private readonly ICommandBus _commandBus;
        private readonly IReadCustomerRepository _customerRepository;

        private readonly IQueryProcessor _queryProcessor;

        public ApplicationAuthorizationServices(
            IQueryProcessor queryProcessor,
            ICommandBus commandBus,
            IReadCustomerRepository customerRepository) {
            _queryProcessor = queryProcessor;
            _commandBus = commandBus;
            _customerRepository = customerRepository;

        }


        public async Task<CustomerReadModel>
            RegisterCustomerAsync(string userName, string password, string invitationCode) {
            var ct = new CancellationToken();

            var existing = await _queryProcessor.ProcessAsync(new GetAuthorizeCustomerQuery(userName), ct);

            if (existing != null) return null;

            if (invitationCode != "TheWinner") return null;

            var id = CustomerId.New;
            var customer = await _commandBus.PublishAsync(new CustomerRegisterCommand(id, new CustomerReadModel {
                EmailAddress = userName,
                //Password = password,
                //InvitationCode = invitationCode,
                //Id = id.GetGuid()
            }), ct);

            return null;

        }

        public async Task<CustomerReadModel> LoginAsync(string email, string password) {

            var user = await _customerRepository.GetAuthorizedCustomerAsync(email);

            if (user == null) return null;

            var account = await _commandBus.PublishAsync(new CustomerLoginCommand(CustomerId.New, email, password),
                CancellationToken.None);


            return default(CustomerReadModel);
        }

    }

}