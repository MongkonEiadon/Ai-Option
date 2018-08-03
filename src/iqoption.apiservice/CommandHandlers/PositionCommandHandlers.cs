using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using iqoption.domain.Positions;
using iqoption.domain.Positions.Commands;

namespace iqoption.apiservice.CommandHandlers {
    public class PositionCommandHandlers :
        ICommandHandler<PositionAggregrate, PositionId, MasterPlacedPositionResult, MasterOpenOrderPositionCommand> {
        public Task<MasterPlacedPositionResult> ExecuteCommandAsync(PositionAggregrate aggregate, MasterOpenOrderPositionCommand command,
            CancellationToken cancellationToken) {


            aggregate.PlacedPosition(command.Position);
            return Task.FromResult(new MasterPlacedPositionResult(true));


        }
    }
}