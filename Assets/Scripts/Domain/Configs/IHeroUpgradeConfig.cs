namespace Assets.Domain.Configs
{
    public interface IHeroUpgradeConfig
    {
        int HealthPerLevel { get; }
        int StrengthPerLevel { get; }
    }
}