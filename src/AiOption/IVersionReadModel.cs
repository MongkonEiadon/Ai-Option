using EventFlow.ReadStores;

namespace AiOption
{
    public interface IVersionReadModel : IReadModel
    {
        long? Version { get; set; }
    }
}