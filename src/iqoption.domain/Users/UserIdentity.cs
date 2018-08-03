using EventFlow.Core;

namespace iqoption.domain.Users {
    public class UserIdentity : Identity<UserIdentity> {
        public UserIdentity(string value) : base(value) {
        }
    }
}