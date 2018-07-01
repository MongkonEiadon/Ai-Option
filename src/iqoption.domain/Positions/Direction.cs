using System.Runtime.Serialization;

namespace iqoption.domain.Positions {
    public enum Direction {
        [EnumMember(Value = "put")] Put = 1,

        [EnumMember(Value = "call")] Call = 2
    }
}