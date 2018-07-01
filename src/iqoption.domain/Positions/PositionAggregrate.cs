using EventFlow.Aggregates;

namespace iqoption.domain.Positions {
    public class PositionAggregrate : AggregateRoot<PositionAggregrate, PositionAggregrateIdentity> {
        public PositionAggregrate(PositionAggregrateIdentity aggregrateIdentity) : base(aggregrateIdentity) {
            AggregrateIdentity = aggregrateIdentity;
        }

        public PositionAggregrateIdentity AggregrateIdentity { get; }
    }
}