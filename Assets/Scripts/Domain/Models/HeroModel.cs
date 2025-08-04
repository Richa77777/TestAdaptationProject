using R3;

namespace Assets.Domain.Models
{
    public class HeroModel
    {
        public ReactiveProperty<int> Level { get; } = new();
        public ReactiveProperty<int> Health { get; } = new();
        public ReactiveProperty<int> Strength { get; } = new();

        public HeroModel(int initialLevel = 1, int initialHealth = 100, int initialStrength = 10)
        {
            Level.Value = initialLevel;
            Health.Value = initialHealth;
            Strength.Value = initialStrength;
        }
    }
}