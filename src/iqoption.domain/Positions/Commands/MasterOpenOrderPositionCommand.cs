using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace iqoption.domain.Positions.Commands {
    public class MasterPlacedPositionResult : IExecutionResult {
        public MasterPlacedPositionResult(bool isSuccess) {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }
    }


    public class MasterOpenOrderPositionCommand : Command<PositionAggregrate, PositionId, MasterPlacedPositionResult> {
        public MasterOpenOrderPositionCommand(PositionId id, OpenPosition position) :
            base(id) {
            Position = position;
        }

        public OpenPosition Position { get; }
    }
}