using System;
using Assets.Domain.Models;
using Assets.Domain.Messages;
using Assets.Application.UseCases;
using Assets.Presentation.Views;
using MessagePipe;
using R3;

namespace Assets.Presentation.Presenters
{
    public class HeroUpgradePresenter : IDisposable
    {
        private readonly HeroUpgradeView _view;
        private readonly HeroModel _heroModel;
        private readonly UpgradeHeroUseCase _upgradeUseCase;
        private readonly IPublisher<UpgradeHeroDTO> _publisher;

        private IDisposable _levelSub;
        private IDisposable _healthSub;
        private IDisposable _strengthSub;

        public HeroUpgradePresenter(
            HeroUpgradeView view,
            HeroModel heroModel,
            UpgradeHeroUseCase upgradeUseCase,
            IPublisher<UpgradeHeroDTO> publisher)
        {
            _view = view;
            _heroModel = heroModel;
            _upgradeUseCase = upgradeUseCase;
            _publisher = publisher;
        }

        public void Start()
        {
            UpdateView();

            _view.SubscribeUpgradeButton(OnUpgradeClicked);

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
                Health = 10,
                Strength = 2
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