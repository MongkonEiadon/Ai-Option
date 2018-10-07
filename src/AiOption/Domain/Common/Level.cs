using EventFlow.ValueObjects;

namespace AiOption.Domain.Common
{
    public enum UserLevel
    {
        Standard = 100,
        Silver = 200,
        Gold = 300,
        Platinum = 400,

        // ReSharper disable once InconsistentNaming
        VIP = 1000,

        Administrator = 9999,

        Banded = -500
    }

    public class Level : SingleValueObject<UserLevel>
    {
        public Level(UserLevel value) : base(value)
        {
        }
    }
}