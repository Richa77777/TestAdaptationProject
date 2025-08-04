using System;
using Assets.Domain.Models;
using Assets.Domain.Messages;
using Assets.Domain.Configs;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Assets.Application.UseCases
{
    public class UpgradeHeroUseCase : IStartable, IDisposable
    {
        private readonly HeroModel _heroModel;
        private readonly ISubscriber<UpgradeHeroDTO> _subscriber;
        private IDisposable _subscription;

        [Inject]
        public UpgradeHeroUseCase(HeroModel heroModel, IHeroUpgradeConfig config, ISubscriber<UpgradeHeroDTO> subscriber)
        {
            _heroModel = heroModel;
            _subscriber = subscriber;
        }

        public void Start()
        {
            _subscription = _subscriber.Subscribe(data =>
            {
                _heroModel.Level.Value += data.Level;
                _heroModel.Health.Value += data.Health;
                _heroModel.Strength.Value += data.Strength;
            });
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}