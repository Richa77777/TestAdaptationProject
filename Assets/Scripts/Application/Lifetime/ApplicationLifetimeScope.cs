using Assets.Application.UseCases;
using VContainer;
using VContainer.Unity;
using UnityEngine;

public class ApplicationLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        Debug.Log("[ApplicationLifetimeScope] Configure");

        builder.Register<UpgradeHeroUseCase>(Lifetime.Singleton);
        Debug.Log("[ApplicationLifetimeScope] Register UpgradeHeroUseCase done");
    }
}