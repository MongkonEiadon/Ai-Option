using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using AiOption.Query.Customers;
using EventFlow.Commands;
using EventFlow.Queries;

namespace AiOption.Domain.Customers.Commands
{
    public class LoginCommand : Command<CustomerAggregate, CustomerId>
    {
        public LoginCommand(CustomerId aggregateId, User user, Password password) : base(aggregateId)
        {
            User = user;
            Password = password;
        }

        public User User { get; }
        public Password Password { get; }
    }

    public class LoginCommandHandler : CommandHandler<CustomerAggregate, CustomerId, LoginCommand>
    {
        private readonly IQueryProcessor _queryProcessor;

        public LoginCommandHandler(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public override async Task ExecuteAsync(CustomerAggregate aggregate, LoginCommand command,
            CancellationToken cancellationToken)
        {
            var user = await _queryProcessor.ProcessAsync(new QueryCustomerByEmailAddress(command.User, true),
                cancellationToken);

            if (user.Password.IsPasswordMatched(command.Password))
            {
                //
            }
        }
    }
}