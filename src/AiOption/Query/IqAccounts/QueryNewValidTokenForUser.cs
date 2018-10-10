using System;
using System.Collections.Generic;
using System.Text;
using AiOption.Domain.Common;
using EventFlow.Queries;

namespace AiOption.Query.IqAccounts
{
    public abstract class QueryNewValidTokenForUser : IQuery<string>
    {
        public User User { get; }
        public Password Password { get; }
        public QueryNewValidTokenForUser(User user, Password password)
        {
            User = user;
            Password = password;
        }
    }
}
