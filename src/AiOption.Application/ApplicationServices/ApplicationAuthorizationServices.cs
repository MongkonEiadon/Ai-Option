using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

using AiOption.Domain.Accounts;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using EventFlow;
using EventFlow.Commands;
using EventFlow.Queries;
using CustomerId = AiOption.Domain.Customers.CustomerId;

namespace AiOption.Application.ApplicationServices {

    public interface IApplicationAuthorizationServices {

        Task<Customer> RegisterCustomerAsync(string userName, string password, string invitationCode);

        Task<Customer> LoginAsync(string userName, string password);

    }


    public class ApplicationAuthorizationServices : IApplicationAuthorizationServices {

        private readonly ICommandBus _commandBus;

        private readonly IQueryProcessor _queryProcessor;

        public ApplicationAuthorizationServices(
            IQueryProcessor queryProcessor,
            ICommandBus commandBus) {
            _queryProcessor = queryProcessor;
            _commandBus = commandBus;

        }


        public Task<Customer> RegisterCustomerAsync(
            string userName, 
            string password, 
            string invitationCode)
        {

            var command = new RequestRegisterCommand(userName, password, invitationCode);
            PublishAsync(command);

            return Task.FromResult(new Customer(CustomerId.New, new User(""), new Password("")));

        }

        public async Task<Customer> LoginAsync(string email, string password) {
            
            

            //var account = await _commandBus.PublishAsync(new LoginCommand(Domain.Customers.CustomerIdentity.New, email, password),
            //    CancellationToken.None);


            return default(Customer);
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