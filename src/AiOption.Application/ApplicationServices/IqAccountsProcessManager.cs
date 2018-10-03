using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Domain.IqAccounts.Commands;
using AiOption.Domain.IqAccounts.Events;
using AiOption.Query.IqAccounts;
using EventFlow;
using EventFlow.Exceptions;
using EventFlow.Queries;

namespace AiOption.Application.ApplicationServices
{
    public class IqAccountsProcessManager
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public IqAccountsProcessManager(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        public async Task ProcessRegisterNewAccountTask(string emailAddress, string password)
        {
            var ct = new CancellationTokenSource();
            try
            {
                var result = await _queryProcessor.ProcessAsync(new QueryIqAccountByEmailAddress(emailAddress), ct.Token);
                if (result != null)
                {
                    throw DomainError.With($"IqAccount with email: {emailAddress} already exists");
                }
            }
            catch (Exception)
            {
                ct.Cancel();
                throw;
            }
        }

        public Task<VerifyIqAccountResult> VerifyIqAccountTask(string emailAddress, string password)
        {
            return _commandBus.PublishAsync(new VerifyIqAccountCommand(emailAddress, password), CancellationToken.None);
        }
    }
}
