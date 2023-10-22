using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills Upgrades/Guardian")]
public class SkillUpgradeSO<T> : ScriptableObject
{
	public List<T> skillSOs;

	public GameObject containerPF;
}
