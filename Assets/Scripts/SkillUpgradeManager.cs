using System.Collections.Generic;
using UnityEngine;

public class SkillUpgradeManager : MonoBehaviour
{
	public List<SkillUpgradeItem> skillUpgradeItems;

	public List<SkillUpgradeItem> appliedDamageUpgrades;

	public List<SkillUpgradeItem> appliedUtilItems;

	public Transform skillButtonSpanwer;

	public int currentSkillIndex = 0;
	

	public void TryUpgrade()
	{
		if (currentSkillIndex == 0)
		{
			SkillUpgradeItem randomUpgrade = GetRandomUpgrade();
			GameObject gameObject = Object.Instantiate(randomUpgrade.containerPF, base.transform.position, Quaternion.identity);
			gameObject.transform.parent = base.transform;
			UpgradeBySkillName(randomUpgrade, gameObject);
			randomUpgrade.currentSkillIndex++;
			currentSkillIndex++;
		}
	}

	private void UpgradeBySkillName(SkillUpgradeItem currentSkillItem, GameObject spawnedSkill)
	{
		if (currentSkillItem.skillName == SkillName.Guardian)
		{
			spawnedSkill.GetComponent<GuardianSkill>().setScriptableObjectProperties(currentSkillItem.skillUpgrades[currentSkillItem.currentSkillIndex] as GuardianSkillSO);
			return;
		}
		Debug.LogWarning("Skill with name " + currentSkillItem.skillName.ToString() + " at index " + currentSkillIndex + " cannot be found");
	}

	public SkillUpgradeItem GetRandomUpgrade()
	{
		return skillUpgradeItems[Random.Range(0, skillUpgradeItems.Count - 1)];
	}
}
