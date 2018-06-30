using EventFlow.Entities;

namespace iqoption.domain.IqOption {
    public class IqOption : Entity<IqOptionIdentity> {

        public string Email { get; set; }
        public string Password { get; set; }

        public IqOption(IqOptionIdentity identity) : base(identity) {
        }
    }
}