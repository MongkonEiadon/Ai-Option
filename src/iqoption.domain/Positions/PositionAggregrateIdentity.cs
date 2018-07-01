using EventFlow.Core;

namespace iqoption.domain.Positions {
    public class PositionAggregrateIdentity : Identity<PositionAggregrateIdentity> {
        public PositionAggregrateIdentity(string value) : base(value) {
        }
    }
}