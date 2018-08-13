using EventFlow.Queries;

namespace iqoption.domain.IqOption.Queries {
    public class GetIqAccountByIqUserIdQuery : IQuery<IqAccount> {
        public int UserId { get; }

        public GetIqAccountByIqUserIdQuery(int userId) {
            UserId = userId;
        }
    }
}