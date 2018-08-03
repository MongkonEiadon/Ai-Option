using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Aggregates;
using EventFlow.Core;

namespace iqoption.domain.IqOption {

    public class IqOptionAggregate : AggregateRoot<IqOptionAggregate, IqOptionIdentity> {
        public IqOptionAggregate(IqOptionIdentity identity) : base(identity) {
        }

       
    }
}
