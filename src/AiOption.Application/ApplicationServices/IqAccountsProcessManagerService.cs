using System;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Domain.Customers;
using AiOption.Domain.IqAccounts.Commands;
using AiOption.Query.IqAccounts;
using EventFlow;
using EventFlow.Exceptions;
using EventFlow.Logs;
using EventFlow.Queries;

namespace AiOption.Application.ApplicationServices
{
    public interface IIqAccountProcessManagerService
    {
        Task ProcessRegisterNewAccountTask(string emailAddress, string password, string token);
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

        public async Task ProcessRegisterNewAccountTask(string emailAddress, string password, string token)
        {
            var ct = new CancellationTokenSource();
            try
            {
                await _commandBus.PublishAsync(new CreateNewIqAccountCommand(
                    CustomerId.With("customer-9e55a180-b3e6-4c6a-a4f5-ea4d910a90b1"),
                    new User(emailAddress),
                    new Password(password),
                    "AnyToken"), ct.Token);
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