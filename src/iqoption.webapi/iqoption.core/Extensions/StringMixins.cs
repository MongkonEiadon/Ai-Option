using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iqoption.core.Extensions
{
    public static class StringMixins
    {
        public static string JoinWithSemicolon(this IEnumerable<string> This)
        {
            if (This == null || !This.Any())
            {
                return string.Empty;
            }

            return string.Join(", ", This);
        }

    }
}
