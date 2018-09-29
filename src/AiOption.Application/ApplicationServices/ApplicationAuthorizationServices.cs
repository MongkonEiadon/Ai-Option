using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Application.Repositories.ReadOnly;
using AiOption.Domain.Account;
using AiOption.Domain.Account.Commands;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using AiOption.Domain.Customers.Queries;
using AiOption.Query.Account;
using EventFlow;
using EventFlow.Commands;
using EventFlow.Queries;

namespace AiOption.Application.ApplicationServices {

    public interface IApplicationAuthorizationServices {

        Task<Account> RegisterCustomerAsync(string userName, string password, string invitationCode);

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


        public Task<Account> RegisterCustomerAsync(
            string userName, 
            string password, 
            string invitationCode)
        {

            var command = new AccountRegisterCommand(userName, password, invitationCode);
            PublishAsync(command);

            return Task.FromResult(new Account(AccountId.New, ""));

        }

        public async Task<CustomerReadModel> LoginAsync(string email, string password) {

            var user = await _customerRepository.GetAuthorizedCustomerAsync(email);

            if (user == null) return null;

            var account = await _commandBus.PublishAsync(new CustomerLoginCommand(CustomerId.New, email, password),
                CancellationToken.None);


            return default(CustomerReadModel);
        }

        [DebuggerStepThrough]
        public void PublishAsync(params ICommand[] commands)
        {
            try
            {
                foreach (var command in commands)
                {
                    command.PublishAsync(_commandBus, CancellationToken.None);
                }
            }
            catch (AggregateException e)
            {
                ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                throw;
            }
        }

        [DebuggerStepThrough]
        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            try
            {
                return _queryProcessor.ProcessAsync(query, CancellationToken.None);
            }
            catch (AggregateException e)
            {
                ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                throw;
            }
        }

    }

}