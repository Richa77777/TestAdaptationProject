using System;
using Assets.Domain.Models;
using Assets.Domain.Messages;
using Assets.Domain.Configs;
using Assets.Presentation.Views;
using MessagePipe;
using R3;
using VContainer;
using VContainer.Unity;

namespace Assets.Presentation.Presenters
{
    public class HeroUpgradePresenter : IDisposable, IStartable
    {
        private readonly HeroUpgradeView _view;
        private readonly HeroModel _heroModel;
        private readonly IPublisher<UpgradeHeroDTO> _publisher;
        private readonly IHeroUpgradeConfig _config;

        private IDisposable _levelSub;
        private IDisposable _healthSub;
        private IDisposable _strengthSub;

        [Inject]
        public HeroUpgradePresenter(
            HeroUpgradeView view,
            HeroModel heroModel,
            IPublisher<UpgradeHeroDTO> publisher,
            IHeroUpgradeConfig config)
        {
            _view = view;
            _heroModel = heroModel;
            _publisher = publisher;
            _config = config;
        }

        public void Start()
        {
            _view.SubscribeUpgradeButton(OnUpgradeClicked);
            UpdateView();

            _levelSub = _heroModel.Level.Subscribe(_ => UpdateView());
            _healthSub = _heroModel.Health.Subscribe(_ => UpdateView());
            _strengthSub = _heroModel.Strength.Subscribe(_ => UpdateView());
        }

        private void UpdateView()
        {
            _view.UpdateStats(_heroModel.Level.Value, _heroModel.Health.Value, _heroModel.Strength.Value);
        }

        private void OnUpgradeClicked()
        {
            _publisher.Publish(new UpgradeHeroDTO
            {
                Level = 1,
                Health = _config.HealthPerLevel,
                Strength = _config.StrengthPerLevel
            });
        }

        public void Dispose()
        {
            _levelSub?.Dispose();
            _healthSub?.Dispose();
            _strengthSub?.Dispose();

            _view.UnsubscribeUpgradeButton(OnUpgradeClicked);
        }
    }
}