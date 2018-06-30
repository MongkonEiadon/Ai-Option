using EventFlow.Aggregates;

namespace iqoption.domain.Users {
    public class UserAggregrate : AggregateRoot<UserAggregrate, UserIdentity> {
        public UserAggregrate(UserIdentity id) : base(id) {
        }
    }
}