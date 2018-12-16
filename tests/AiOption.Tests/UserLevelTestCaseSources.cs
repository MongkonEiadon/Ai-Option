using System;
using System.Collections;
using System.Linq;
using AiOption.Domain.Common;

namespace AiOption.Tests
{
    public class UserLevelTestCaseSources : IEnumerable
    {
        public static UserLevel[] UserLevels =>
            Enum.GetValues(typeof(UserLevel))
                .Cast<UserLevel>()
                .ToArray();

        public IEnumerator GetEnumerator()
        {
            return UserLevels.GetEnumerator();
        }
    }
}