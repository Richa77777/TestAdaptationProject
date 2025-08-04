using Cysharp.Threading.Tasks;
using Assets.Domain.Models;
using Assets.Domain.Messages;
using Assets.Infrastructure.Configs;

namespace Assets.Application.UseCases
{
    public class UpgradeHeroUseCase
    {
        private readonly HeroModel _hero;
        private readonly HeroUpgradeConfigSO _config;

        public UpgradeHeroUseCase(HeroModel hero, HeroUpgradeConfigSO config)
        {
            _hero = hero;
            _config = config;
        }

        public UniTask ExecuteAsync(UpgradeHeroDTO upgradeData)
        {
            _hero.Level.Value += upgradeData.Level;
            _hero.Health.Value += upgradeData.Health;
            _hero.Strength.Value += upgradeData.Strength;

            return UniTask.CompletedTask;
        }
    }
}