using System.Collections.Generic;
using Enums;
using StatsPackage;
using UnityEngine;

namespace Skills
{
    public static class StatsHelpers
    {
        public static bool isStatText(Stat stat)
        {
            return stat.statType is StatType.name or StatType.description;
        }
        
        public static bool isStatSprite(Stat stat)
        {
            return stat.statType is StatType.childSprite or StatType.parentSprite;
        }
        
        public static bool isStatFloat(Stat stat)
        {
            console.log(stat + "stats");
            return !isStatText(stat) && !isStatText(stat);
        }
        
        public static List<T> GetScriptableObjects<T>(string path) where T : ScriptableObject
        {
            #if UNITY_EDITOR


            string[] guids = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(T).ToString(), new[] { path });
            List<T> scriptableObjects = new List<T>();

            foreach (var guid in guids)
            {
                UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                scriptableObjects.Add(UnityEditor.AssetDatabase.LoadAssetAtPath(assetPath, typeof(T)) as T);
            }

            return scriptableObjects;
            #else
            return null;
            #endif
        }
    }
}