using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace iqoption.domain.Positions.Commands {
    public class OrderPlaceResult : IExecutionResult {
        public OrderPlaceResult(bool isSuccess) {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }
    }


    public class
        MasterOpenOrderPositionCommand : Command<PositionAggregrate, PositionAggregrateIdentity, OrderPlaceResult> {
        public MasterOpenOrderPositionCommand(PositionAggregrateIdentity aggregrateIdentity, OpenPosition position) :
            base(aggregrateIdentity) {
            Position = position;
        }

        public OpenPosition Position { get; }
    }
}