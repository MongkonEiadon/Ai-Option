using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Subscribers;

namespace iqoption.domain.Positions.Events {
    public class MasterPlacePositionCompleteEvent : AggregateEvent<PositionAggregrate, PositionId> {

        public OpenPosition OpenedPosition { get; }

        public MasterPlacePositionCompleteEvent(OpenPosition openedPosition) {
            OpenedPosition = openedPosition;
        }
    }

    public class MasterPlaceEventSubscribe : ISubscribeAsynchronousTo<PositionAggregrate, PositionId,
        MasterPlacePositionCompleteEvent> {
        public Task HandleAsync(
            IDomainEvent<PositionAggregrate, PositionId, MasterPlacePositionCompleteEvent> domainEvent,
            CancellationToken cancellationToken) {


            return Task.CompletedTask;
        }
    }
}