using System.Threading;
using System.Threading.Tasks;
using AiOption.Application.API;
using AiOption.Query.IqAccounts;
using EventFlow.Exceptions;
using EventFlow.Queries;

namespace AiOption.Application.QueryHandlers.IqAccounts
{
    internal class IqAccountsQueryHandlers : IQueryHandler<QueryNewValidTokenForUser, string>
    {
        private readonly IIqOptionApiWrapper _apiWrapper;

        public IqAccountsQueryHandlers(IIqOptionApiWrapper apiWrapper)
        {
            _apiWrapper = apiWrapper;
        }

        public async Task<string> ExecuteQueryAsync(QueryNewValidTokenForUser query,
            CancellationToken cancellationToken)
        {
            var result = await _apiWrapper.LoginToIqOptionAsync(query.User.Value, query.Password.Value);

            if (!result.Item1) throw DomainError.With($"Get credential for '{query.User}' failed with {result.Item2}");

            return result.Item2;
        }
    }
}