using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Subscribers;

namespace iqoption.domain.Positions.Events {
    public class MasterPlacePositionCompleteEvent : AggregateEvent<PositionAggregrate, PositionAggregrateIdentity> {
        protected MasterPlacePositionCompleteEvent() {
        }
    }

    public class MasterPlaceEventSubscribe : ISubscribeAsynchronousTo<PositionAggregrate, PositionAggregrateIdentity,
        MasterPlacePositionCompleteEvent> {
        public Task HandleAsync(
            IDomainEvent<PositionAggregrate, PositionAggregrateIdentity, MasterPlacePositionCompleteEvent> domainEvent,
            CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }
}