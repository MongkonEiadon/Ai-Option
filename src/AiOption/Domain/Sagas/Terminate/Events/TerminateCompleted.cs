using EventFlow.Aggregates;

namespace AiOption.Domain.Sagas.Terminate.Events
{
    public class TerminateCompleted : AggregateEvent<TerminateSaga, TerminateSagaId>
    {
    }
}