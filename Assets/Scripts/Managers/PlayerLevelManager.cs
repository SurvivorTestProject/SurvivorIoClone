using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
	public class PlayerLevelManager : MonoBehaviour
	{
		public float currentExp;

		public int currentLevel = 1;

		public float nextLevelExpMultiplier;

		public float expToNextLevel = 50f;
		public event Action LeveledUp;
		
		[SerializeField]
		private SkillUpgradeManager skillUpgradeManager;

		public TextMeshProUGUI levelTextUI;

		public Image healthBar;

		public int GetCurrentLevel()
		{
			return currentLevel;
		}

		public void AddExp(int gainedExp)
		{
			currentExp += gainedExp;
			while (currentExp > expToNextLevel)
			{
				currentLevel++;
				currentExp -= expToNextLevel;
				expToNextLevel *= nextLevelExpMultiplier;
				LeveledUp?.Invoke();
			}
			healthBar.transform.localScale = new Vector3(currentExp / expToNextLevel % 1f, 1f, 1f);
			levelTextUI.text = currentLevel.ToString();
		}
	}
}
