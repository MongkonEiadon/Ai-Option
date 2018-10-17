using EventFlow.Aggregates;

namespace AiOption.Domain.Sagas.Terminate.Events
{
    public class TerminateStarted :AggregateEvent<TerminateSaga, TerminateSagaId>
    {
    }
}