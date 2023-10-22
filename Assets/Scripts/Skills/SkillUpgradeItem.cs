using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillUpgradeItem
{
	public SkillName skillName;

	public SkillType skillType;

	public GameObject containerPF;

	public List<SkillSO> skillUpgrades;

	public int currentSkillIndex;

	private GameObject containerGO;

	public GameObject getContainerGO()
	{
		return this.containerGO;
	}

	public void SetGameObject(GameObject value)
	{
		this.containerGO = value;
	}
}
