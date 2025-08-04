using UnityEngine;
using VContainer;
using VContainer.Unity;
using Assets.Domain.Models;
using Assets.Domain.Messages;
using Assets.Infrastructure.Configs;
using MessagePipe;

namespace Assets.Infrastructure.Lifetime
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private HeroUpgradeConfigSO _upgradeConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            var heroModel = new HeroModel();

            builder.RegisterInstance(heroModel);
            builder.RegisterInstance(_upgradeConfig);

            builder.Register<MessageBroker<UpgradeHeroDTO>>(VContainer.Lifetime.Singleton);

            builder.Register<IPublisher<UpgradeHeroDTO>>(c => 
                c.Resolve<MessageBroker<UpgradeHeroDTO>>(), VContainer.Lifetime.Singleton);

            builder.Register<ISubscriber<UpgradeHeroDTO>>(c => 
                c.Resolve<MessageBroker<UpgradeHeroDTO>>(), VContainer.Lifetime.Singleton);
        }
    }
}