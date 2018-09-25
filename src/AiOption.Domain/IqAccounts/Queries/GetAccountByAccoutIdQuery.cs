using EventFlow.Queries;

namespace AiOption.Domain.IqAccounts.Queries {

    public class GetAccountByAccoutIdQuery : IQuery<Account> {

        public GetAccountByAccoutIdQuery(int userId) {
            UserId = userId;
        }

        public int UserId { get; }

    }

}