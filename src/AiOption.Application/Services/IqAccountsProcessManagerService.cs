using System;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;
using AiOption.Query.IqAccounts;
using EventFlow;
using EventFlow.Queries;

namespace AiOption.Application.Services
{
    public interface IIqAccountProcessManagerService
    {
        Task ProcessRegisterNewAccountTask(CustomerId customerId, string emailAddress, string password, string token);
    }

    public class IqAccountsProcessManagerService : IIqAccountProcessManagerService,
        IQueryHandler<QueryNewValidTokenForUser, string>
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public IqAccountsProcessManagerService(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        public async Task ProcessRegisterNewAccountTask(CustomerId customerId, string emailAddress, string password,
            string token)
        {
            var ct = new CancellationTokenSource();
            try
            {
                await _commandBus.PublishAsync(new CreateNewIqAccountCommand(
                    customerId,
                    Email.New(emailAddress),
                    Password.New(password)), ct.Token);
            }
            catch (Exception ex)
            {
                ct.Cancel();
                throw;
            }
        }


        public Task<string> ExecuteQueryAsync(QueryNewValidTokenForUser query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}