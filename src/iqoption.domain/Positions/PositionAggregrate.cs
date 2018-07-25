using System.Collections.Generic;
using EventFlow.Aggregates;
using iqoption.domain.Positions.Events;

namespace iqoption.domain.Positions {
    public class PositionAggregrate : AggregateRoot<PositionAggregrate, PositionId> {
        private static readonly List<OpenPosition> OpenedPosition = new List<OpenPosition>();
        public IReadOnlyCollection<OpenPosition> OpenedPositions => OpenedPosition;

        public PositionAggregrate(PositionId id) : base(id) {
            Id = id;

            
            Register<MasterPlacePositionCompleteEvent>(e => OpenedPosition.Add(e.OpenedPosition));
          

        }


        public void PlacedPosition(OpenPosition position) {
            Emit(new MasterPlacePositionCompleteEvent(position));
        }

        public PositionId Id { get; }
    }
}