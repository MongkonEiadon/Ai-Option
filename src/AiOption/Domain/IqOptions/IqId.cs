using EventFlow.Core;

namespace AiOption.Domain.IqOptions
{
    public class IqId : Identity<IqId> {
        public IqId(string value) : base(value) { }
    }
}