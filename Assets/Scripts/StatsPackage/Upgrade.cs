using UnityEngine;

namespace StatsPackage
{
    [CreateAssetMenu(menuName = "Upgrade")]
    public abstract class Upgrade : ScriptableObject
    {
        public Sprite upgradeIcon;
        public string upgradeName;
        public string description;

        public abstract void DoUpgrade();
    }
}



