using System;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Application.Repositories.ReadOnly;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;

using EventFlow;
using EventFlow.Queries;

namespace AiOption.Application.ApplicationServices
{

    public interface IApplicationAuthorizationServices {

        Task<AuthorizedCustomer> LoginAsync(string userName, string password);

    }
    public class ApplicationAuthorizationServices : IApplicationAuthorizationServices
    {

        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandBus _commandBus;
        private readonly IReadCustomerRepository _customerRepository;

        public ApplicationAuthorizationServices(IQueryProcessor queryProcessor, ICommandBus commandBus, IReadCustomerRepository customerRepository) {
            _queryProcessor = queryProcessor;
            _commandBus = commandBus;
            _customerRepository = customerRepository;

        }
        

        public async Task<AuthorizedCustomer> LoginAsync(string email, string password) {

            var user = await _customerRepository.GetAuthorizedCustomerAsync(email);

            if (user == null) {
                return null;
            }

            var account = await _commandBus.PublishAsync(new LoginCommand(CustomerId.New, email, password), CancellationToken.None);
            

            return default(AuthorizedCustomer);
        }

    }
}
