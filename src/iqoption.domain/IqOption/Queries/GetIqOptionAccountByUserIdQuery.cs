using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Queries;

namespace iqoption.domain.IqOption.Queries
{

    public class GetIqOptionAccountByUserIdQuery : IQuery<IEnumerable<IqAccount>> {
        public string UserName { get; }
        public GetIqOptionAccountByUserIdQuery(string userName)
        {
            UserName = userName;
        }
    }
}
