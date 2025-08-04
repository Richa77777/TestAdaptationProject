using Assets.Domain.Models;
using Assets.Domain.Messages;
using Assets.Domain.Configs;
using Assets.Infrastructure.Configs;
using Assets.Application.UseCases;
using Assets.Presentation.Views;
using Assets.Presentation.Presenters;
using VContainer;
using VContainer.Unity;
using MessagePipe;
using UnityEngine;

namespace Assets.Infrastructure.LifetimeScopes
{
    public class InfrastructureLifetimeScope : LifetimeScope
    {
        [SerializeField] private HeroUpgradeConfigSO _upgradeConfig;
        [SerializeField] private HeroUpgradeView _heroUpgradeView;

        protected override void Configure(IContainerBuilder builder)
        {
            var heroModel = new HeroModel();
            builder.RegisterInstance(heroModel);

            var options = builder.RegisterMessagePipe();
            builder.RegisterMessageBroker<UpgradeHeroDTO>(options);

            builder.RegisterInstance<IHeroUpgradeConfig>(_upgradeConfig);

            builder.RegisterEntryPoint<UpgradeHeroUseCase>();
            builder.RegisterEntryPoint<HeroUpgradePresenter>();

            builder.RegisterComponent(_heroUpgradeView);
        }
    }

}