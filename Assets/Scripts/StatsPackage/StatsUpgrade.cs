using System.Collections.Generic;
using UnityEngine;

namespace StatsPackage
{
    [CreateAssetMenu(menuName = "Skills/Stat Upgrade")]
    public class StatsUpgrade : Upgrade
    {
        [SerializeField]
        public List<Stats> unitsToUpgrade = new List<Stats>();
        public List<Stat> upgradeToApply = new List<Stat>();
        public bool isPercentUpgrade = false;
        public bool isInstantiator = false;
        public GameObject instatiatorGO;
    

        public override void DoUpgrade()
        {
            foreach (var unitToUpgrade in unitsToUpgrade)
            {
                unitToUpgrade.UnlockUpgrade(this);
            }
        }
    }
}

