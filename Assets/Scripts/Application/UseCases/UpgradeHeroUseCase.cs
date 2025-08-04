using System;
using Assets.Domain.Models;
using Assets.Domain.Messages;
using Assets.Infrastructure.Configs;
using MessagePipe;
using UnityEngine;
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
            Debug.Log("[UpgradeHeroUseCase] Start subscribing to UpgradeHeroDTO");

            _subscription = _subscriber.Subscribe(data =>
            {
                Debug.Log($"[UpgradeHeroUseCase] Received Upgrade: Lvl +{data.Level}, HP +{data.Health}, Str +{data.Strength}");

                _hero.Level.Value += data.Level;
                _hero.Health.Value += data.Health;
                _hero.Strength.Value += data.Strength;
            });
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}