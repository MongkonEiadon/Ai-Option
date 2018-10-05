using EventFlow.ValueObjects;

namespace AiOption.Domain.Common
{
    public class Token : SingleValueObject<string>
    {
        private const string Secret = "AiOption";

        public Token(string value) : base(value)
        {
        }
    }
}