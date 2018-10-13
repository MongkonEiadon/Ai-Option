using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using AiOption.Query.Customers;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Core;
using EventFlow.Queries;

namespace AiOption.Application.ApplicationServices
{
    public interface ICustomerProcessManagerService
    {
        Task<Customer> RegisterCustomerAsync(string emailAddress, string password, string invitationCode);

        Task<Customer> ChangeCustomerLevel(CustomerId customerId, Level level);
    }
    public class CustomerProcessManagerService : ICustomerProcessManagerService {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public CustomerProcessManagerService(
            IQueryProcessor queryProcessor,
            ICommandBus commandBus)
        {
            _queryProcessor = queryProcessor;
            _commandBus = commandBus;
        }


        public async Task<Customer> RegisterCustomerAsync(
            string emailAddress,
            string password,
            string invitationCode)
        {
            // register
            var command = await PublishAsync(new CustomerRegisterCommand(emailAddress, password, invitationCode));

            // get new customer
            var result = await QueryAsync(new QueryCustomerByEmailAddress(new Email(emailAddress)));

            //change level to standard
            await PublishAsync(new ChangeLevelCommand(result.Id, new Level(UserLevel.Standard)));

            // get new customer
            result = await QueryAsync(new QueryCustomerById(result.Id));

            return result;
        }
    
        public async Task<Customer> ChangeCustomerLevel(CustomerId customerId, Level level)
        {
            //command to change customer level
            await PublishAsync(new ChangeLevelCommand(customerId, level));

            //query back
            return await QueryAsync(new QueryCustomerById(customerId));
        }


        [DebuggerStepThrough]
        private Task<TResult> PublishAsync<TAggregate, TIdentity, TResult>(ICommand<TAggregate, TIdentity, TResult> command)
            where TAggregate : IAggregateRoot<TIdentity>
            where TIdentity : IIdentity 
            where TResult : IExecutionResult
        {
            return _commandBus.PublishAsync(command, CancellationToken.None);
        }

        [DebuggerStepThrough]
        private Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            return _queryProcessor.ProcessAsync(query, CancellationToken.None);
        }
    }
}