using System;
using System.Collections.Generic;
using Enums;
using Skills;
using UnityEngine;

namespace StatsPackage
{
    [CreateAssetMenu(menuName = "Skills/Stats")]
    public class Stats : ScriptableObject
    {
        public List<Stat> instanceStats = new List<Stat>();
        public List<Stat> stats = new List<Stat>();
        public List<StatsUpgrade> appliedUpgrades = new List<StatsUpgrade>();

        public event Action<Stats, StatsUpgrade> upgradeApplied;

        public float GetStatFloat(StatType stat)
        {
            Stat foundInstanceStat = instanceStats.Find(item => item.statType == stat);
            if (foundInstanceStat is not null)
            {
                if (StatsHelpers.isStatFloat(foundInstanceStat))
                {
                    return GetUpgradedValue(foundInstanceStat.statType, foundInstanceStat.value);
                }
            }
        
            Stat foundStat = stats.Find(item => item.statType == stat);
            if (foundStat is not null)
            {
                if (StatsHelpers.isStatFloat(foundStat))
                {
                    return GetUpgradedValue(foundStat.statType, foundStat.value);
                }
            }

            return 0;
        }
    
        public Sprite GetStatSprite(StatType stat)
        {
            Stat foundStat = stats.Find(item => item.statType == stat);
            if (foundStat is not null)
            {
                if (StatsHelpers.isStatSprite(foundStat))
                {
                    return GetUpgradedValueSprite(foundStat.statType, foundStat.sprite);
                }
            }

            return null;
        }
    
        public void UnlockUpgrade(StatsUpgrade upgrade)
        {
            appliedUpgrades.Add(upgrade);
            upgradeApplied?.Invoke(this, upgrade);
        }

        private Sprite GetUpgradedValueSprite(StatType stat, Sprite baseSprite)
        {
            foreach (var upgrade in appliedUpgrades)
            {
                Stat foundStat = upgrade.upgradeToApply.Find(item => item.statType == stat);
                if(foundStat is not null)
                {
                    if (StatsHelpers.isStatSprite(foundStat))
                    {
                        if (foundStat.sprite is not null)
                            baseSprite = foundStat.sprite;
                    }
                }
            }

            return baseSprite;
        }

        private float GetUpgradedValue(StatType stat, float baseValue)
        {
            foreach (var upgrade in appliedUpgrades)
            {
                Stat foundStat = upgrade.upgradeToApply.Find(item => item.statType == stat);
                if(foundStat is not null)
                {
                    if (StatsHelpers.isStatFloat(foundStat))
                    {
                        if (upgrade)
                            baseValue *= (foundStat.value / 100f) + 1f;
                        else
                            baseValue += foundStat.value;
                    }
                }
            }

            return baseValue;
        }

        public void ResetAppliedUpgrades()
        {
            appliedUpgrades.Clear();
        }
    }
}

