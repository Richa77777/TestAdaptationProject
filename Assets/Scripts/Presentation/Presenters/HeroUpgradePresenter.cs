using System;
using Assets.Domain.Models;
using Assets.Domain.Messages;
using Assets.Application.UseCases;
using Assets.Infrastructure.Configs;
using Assets.Presentation.Views;
using MessagePipe;
using R3;
using UnityEngine;

namespace Assets.Presentation.Presenters
{
    public class HeroUpgradePresenter : IDisposable
    {
        private readonly HeroUpgradeView _view;
        private readonly HeroModel _heroModel;
        private readonly UpgradeHeroUseCase _upgradeUseCase;
        private readonly IPublisher<UpgradeHeroDTO> _publisher;
        private readonly HeroUpgradeConfigSO _config;

        private IDisposable _levelSub;
        private IDisposable _healthSub;
        private IDisposable _strengthSub;

        public HeroUpgradePresenter(
            HeroUpgradeView view,
            HeroModel heroModel,
            UpgradeHeroUseCase upgradeUseCase,
            IPublisher<UpgradeHeroDTO> publisher,
            HeroUpgradeConfigSO config)
        {
            _view = view;
            _heroModel = heroModel;
            _upgradeUseCase = upgradeUseCase;
            _publisher = publisher;
            _config = config;
        }

        public void Start()
        {
            Debug.Log("[HeroUpgradePresenter] Start called");

            UpdateView();

            _view.SubscribeUpgradeButton(OnUpgradeClicked);

            _levelSub = _heroModel.Level.Subscribe(value =>
            {
                Debug.Log($"[HeroUpgradePresenter] Level updated: {value}");
                UpdateView();
            });
            _healthSub = _heroModel.Health.Subscribe(value =>
            {
                Debug.Log($"[HeroUpgradePresenter] Health updated: {value}");
                UpdateView();
            });
            _strengthSub = _heroModel.Strength.Subscribe(value =>
            {
                Debug.Log($"[HeroUpgradePresenter] Strength updated: {value}");
                UpdateView();
            });
        }

        private void UpdateView()
        {
            Debug.Log($"[HeroUpgradePresenter] UpdateView called: Level={_heroModel.Level.Value}, Health={_heroModel.Health.Value}, Strength={_heroModel.Strength.Value}");
            _view.UpdateStats(_heroModel.Level.Value, _heroModel.Health.Value, _heroModel.Strength.Value);
        }

        private void OnUpgradeClicked()
        {
            Debug.Log("[HeroUpgradePresenter] Upgrade button clicked");

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