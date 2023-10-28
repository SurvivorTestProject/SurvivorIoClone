using System.Collections.Generic;
using Skills;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using StatsPackage;

namespace Managers
{
	public class SkillUpgradeManager : MonoBehaviour
	{
		public List<SkillUpgradeItem> skillUpgradeItems;
		public List<SkillUpgradeItem> appliedDamageUpgrades;
		public List<SkillUpgradeItem> appliedUtilItems;
		[SerializeField] private GameObject skillButtonPF;
		[SerializeField] private Transform skillButtonSpawner;
		[SerializeField] private StatsUpgrade statsUpgrade;
		[SerializeField] private PlayerLevelManager playerLevelManager;

		private void Awake()
		{
			playerLevelManager.LeveledUp += TryGetUpgrade;
		}

		public void TryGetUpgrade()
		{
			

			SkillUpgradeItem currentUpgrade=this.GetRandomUpgrade();
			if (currentUpgrade != null&&currentUpgrade.skillUpgrades.Count>currentUpgrade.currentSkillIndex)
			{
				GameManager.Instance.SetGameState(GameState.UPGRADE_MENU);
				SpawnSkillButton(currentUpgrade);
			}
		}

		private void InstantiateUpgradeGO(SkillUpgradeItem currentUpgrade)
		{
			GameObject containerGo;
			if(currentUpgrade.currentSkillIndex==0)
			{
				containerGo = Instantiate(currentUpgrade.containerPF, transform.position, Quaternion.identity);
				containerGo.transform.parent = transform;
				currentUpgrade.SetGameObject(containerGo);
			}
			else
			{
				containerGo = currentUpgrade.getContainerGO();
			}
			UpgradeBySkillName(currentUpgrade, containerGo);
			if (IsFullyUpgraded(currentUpgrade))
			{
				skillUpgradeItems.Remove(currentUpgrade);
			}
			else
			{
				currentUpgrade.currentSkillIndex++;
			}
		}

		private bool IsFullyUpgraded(SkillUpgradeItem currentUpgrade)
		{
			return currentUpgrade.currentSkillIndex == (currentUpgrade.skillUpgrades.Count - 1);
		}
		
		private void UpgradeBySkillName(SkillUpgradeItem currentSkillItem, GameObject spawnedSkill)
		{
			switch (currentSkillItem.skillName)
			{
				case SkillName.Guardian:
					spawnedSkill.GetComponent<GuardianSkill>().setScriptableObjectProperties(currentSkillItem.skillUpgrades[currentSkillItem.currentSkillIndex]);
					break;
				default:
					break;
			}
		}

		private SkillUpgradeItem GetRandomUpgrade()
		{
			return skillUpgradeItems[Random.Range(0, skillUpgradeItems.Count - 1)];
		}
		
		private void SpawnSkillButton(SkillUpgradeItem skillUpgradeItem)
		{
			SkillSO skillSo = skillUpgradeItem.skillUpgrades[skillUpgradeItem.currentSkillIndex];
			if (skillSo!=null && skillSo.upgradeButtonSprite)
			{
				Sprite upgradeSprite = skillSo.upgradeButtonSprite;
				GameObject skillButton = Instantiate(skillButtonPF, skillButtonSpawner.position,
					Quaternion.identity);
				skillButton.transform.parent = skillButtonSpawner;
				skillButton.GetComponent<Image>().sprite = upgradeSprite;
				skillButton.GetComponent<Button>().onClick.AddListener(()=>
				{
					InstantiateUpgradeGO(skillUpgradeItem);
					statsUpgrade.DoUpgrade();
					GameManager.Instance.SetGameState(GameState.PLAYING);
					Destroy(skillButton);
				});
			}
		}
		
		private void OnDestroy()
		{
			playerLevelManager.LeveledUp -= TryGetUpgrade;
		}
	}
}
