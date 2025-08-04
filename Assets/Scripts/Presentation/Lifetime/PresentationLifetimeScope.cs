using Assets.Domain.Models;
using Assets.Infrastructure.Configs;
using Assets.Application.UseCases;
using Assets.Presentation.Views;
using Assets.Presentation.Presenters;
using VContainer;
using VContainer.Unity;
using UnityEngine;

public class PresentationLifetimeScope : LifetimeScope
{
    [SerializeField] private HeroUpgradeView _heroUpgradeView;

    private HeroUpgradePresenter _heroUpgradePresenter;
    private UpgradeHeroUseCase _upgradeHeroUseCase;

    protected override void Configure(IContainerBuilder builder)
    {
        Debug.Log("[PresentationLifetimeScope] Configure");

        builder.RegisterComponent(_heroUpgradeView);
        Debug.Log("[PresentationLifetimeScope] RegisterComponent(HeroUpgradeView) done");

        builder.Register<HeroUpgradePresenter>(Lifetime.Singleton)
            .WithParameter(_heroUpgradeView)
            .WithParameter(c => c.Resolve<HeroModel>())
            .WithParameter(c => c.Resolve<UpgradeHeroUseCase>())
            .WithParameter(c => c.Resolve<HeroUpgradeConfigSO>());
        Debug.Log("[PresentationLifetimeScope] Register HeroUpgradePresenter done");
    }

    protected void Start()
    {
        Debug.Log("[PresentationLifetimeScope] Start - resolve UseCase and Presenter");

        _upgradeHeroUseCase = Container.Resolve<UpgradeHeroUseCase>();
        _upgradeHeroUseCase.Start();

        _heroUpgradePresenter = Container.Resolve<HeroUpgradePresenter>();
        _heroUpgradePresenter.Start();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _heroUpgradePresenter?.Dispose();
    }
}