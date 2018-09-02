using EventFlow.Queries;

namespace AiOption.Domain.IqAccounts.Queries {

    public class GetAccountByAccoutIdQuery : IQuery<Account> {

        public int UserId { get; }

        public GetAccountByAccoutIdQuery(int userId) {
            UserId = userId;
        }
    }

}