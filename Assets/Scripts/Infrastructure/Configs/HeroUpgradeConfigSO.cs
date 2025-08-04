using UnityEngine;
using Assets.Domain.Configs;

namespace Assets.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "HeroUpgradeConfig", menuName = "Configs/Hero Upgrade Config")]
    public class HeroUpgradeConfigSO : ScriptableObject, IHeroUpgradeConfig
    {
        [field: SerializeField] public int HealthPerLevel { get; private set; } = 10;
        [field: SerializeField] public int StrengthPerLevel { get; private set; } = 2;
    }
}