using Assets.Domain.Models;
using Assets.Infrastructure.Configs;
using Assets.Domain.Messages;
using MessagePipe;
using VContainer;
using VContainer.Unity;
using UnityEngine;

public class InfrastructureLifetimeScope : LifetimeScope
{
    [SerializeField] private HeroUpgradeConfigSO _upgradeConfig;

    protected override void Configure(IContainerBuilder builder)
    {
        Debug.Log("[InfrastructureLifetimeScope] Configure");

        var options = builder.RegisterMessagePipe();
        Debug.Log("[InfrastructureLifetimeScope] RegisterMessagePipe done");

        builder.RegisterMessageBroker<UpgradeHeroDTO>(options);
        Debug.Log("[InfrastructureLifetimeScope] RegisterMessageBroker<UpgradeHeroDTO> done");

        builder.RegisterInstance(_upgradeConfig);
        Debug.Log("[InfrastructureLifetimeScope] RegisterInstance(HeroUpgradeConfigSO) done");

        var heroModel = new HeroModel();
        builder.RegisterInstance(heroModel);
        Debug.Log("[InfrastructureLifetimeScope] RegisterInstance(HeroModel) done");
    }
}