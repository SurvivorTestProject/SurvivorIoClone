using System;
using Enums;
using UnityEngine;

namespace StatsPackage
{
    [Serializable]
    public class Stat
    {
        public StatType statType;
        public float value = 0;
        public Sprite sprite;
        public string stringValue;
    }
}