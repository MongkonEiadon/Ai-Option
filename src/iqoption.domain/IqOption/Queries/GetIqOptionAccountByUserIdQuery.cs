using System.Collections.Generic;
using EventFlow.Queries;

namespace iqoption.domain.IqOption.Queries {
    public class GetIqOptionAccountByUserIdQuery : IQuery<IEnumerable<IqAccount>> {
        public GetIqOptionAccountByUserIdQuery(string userName) {
            UserName = userName;
        }

        public string UserName { get; }
    }

    public class InActiveAccountQuery : IQuery<List<IqAccount>> {
    }

    public class ActiveAccountQuery : IQuery<List<IqAccount>> {
    }
}