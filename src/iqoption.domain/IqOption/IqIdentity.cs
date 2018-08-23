using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace iqoption.domain.IqOption {
    public class IqIdentity : Identity<IqIdentity> {
        
        public IqIdentity(string value) : base(value) {
        }
    }
}