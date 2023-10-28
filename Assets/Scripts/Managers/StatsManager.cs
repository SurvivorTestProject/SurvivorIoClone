using System.Collections.Generic;
using Skills;
using StatsPackage;
using UnityEngine;

namespace Managers
{
    public class StatsManager : MonoBehaviour
    {
        [SerializeField] private List<Stats> statsList;
        private const string statsPath = "Assets/SOInstances/Stats";

        private void OnApplicationQuit()
        {
            statsList = StatsHelpers.GetScriptableObjects<Stats>(statsPath);
        
            foreach (var stat in statsList)
            {
                stat.ResetAppliedUpgrades();
            }
        }

    }
}
