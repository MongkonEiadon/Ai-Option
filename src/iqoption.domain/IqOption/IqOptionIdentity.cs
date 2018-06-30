using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace iqoption.domain.IqOption {
    public class IqOptionIdentity : Identity<IqOptionIdentity> {
        
        public IqOptionIdentity(string value) : base(value) {
        }
    }
}