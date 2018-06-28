using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace iqoption.core.Extensions {
    public static class StringMixins {
        public static string JoinWithSemicolon(this IEnumerable<string> This) {
            if (This == null || !This.Any()) {
                return string.Empty;
            }

            return string.Join(", ", This);
        }

        public static string AsJson(this object This) {
            return JsonConvert.SerializeObject(This);
        }

        public static T JsonAs<T>(this string This) {
            return JsonConvert.DeserializeObject<T>(This);
        }
    }
}