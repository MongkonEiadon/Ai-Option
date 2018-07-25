using EventFlow.Core;

namespace iqoption.domain.Positions {
    public class PositionId : Identity<PositionId> {
        public PositionId(string value) : base(value) {
        }
    }
}