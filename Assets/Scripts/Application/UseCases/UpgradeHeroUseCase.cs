using System;
using Cysharp.Threading.Tasks;
using Assets.Domain.Models;
using Assets.Domain.Messages;
using Assets.Infrastructure.Configs;
using MessagePipe;
using VContainer.Unity;

namespace Assets.Application.UseCases
{
    public class UpgradeHeroUseCase : IStartable, IDisposable
    {
        private readonly HeroModel _hero;
        private readonly HeroUpgradeConfigSO _config;
        private readonly ISubscriber<UpgradeHeroDTO> _subscriber;
        private IDisposable _subscription;

        public UpgradeHeroUseCase(HeroModel hero, HeroUpgradeConfigSO config, ISubscriber<UpgradeHeroDTO> subscriber)
        {
            _hero = hero;
            _config = config;
            _subscriber = subscriber;
        }

        public void Start()
        {
            _subscription = _subscriber.Subscribe(async data =>
            {
                await ExecuteAsync(data);
            });
        }

        public UniTask ExecuteAsync(UpgradeHeroDTO upgradeData)
        {
            _hero.Level.Value += upgradeData.Level;
            _hero.Health.Value += upgradeData.Health;
            _hero.Strength.Value += upgradeData.Strength;

            return UniTask.CompletedTask;
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}