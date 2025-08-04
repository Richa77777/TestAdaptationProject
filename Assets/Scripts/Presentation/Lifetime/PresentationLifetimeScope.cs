using Assets.Application.UseCases;
using Assets.Domain.Models;
using Assets.Presentation.Presenters;
using Assets.Presentation.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PresentationLifetimeScope : LifetimeScope
{
    [SerializeField] private HeroUpgradeView _heroUpgradeView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_heroUpgradeView);

        builder.Register<UpgradeHeroUseCase>(VContainer.Lifetime.Singleton);
        
        builder.Register<HeroUpgradePresenter>(VContainer.Lifetime.Singleton)
            .WithParameter(_heroUpgradeView)
            .WithParameter(c => c.Resolve<HeroModel>())
            .WithParameter(c => c.Resolve<UpgradeHeroUseCase>());
    }
}
