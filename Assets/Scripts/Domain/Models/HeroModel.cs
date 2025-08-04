using R3;

namespace Assets.Domain.Models
{
    public class HeroModel
    {
        public ReactiveProperty<int> Level { get; } = new();
        public ReactiveProperty<int> Health { get; } = new();
        public ReactiveProperty<int> Strength { get; } = new();

        public HeroModel()
        {
            Level.Value = 1; // Initial Values
            Health.Value = 100;
            Strength.Value = 10;
        }
    }
}