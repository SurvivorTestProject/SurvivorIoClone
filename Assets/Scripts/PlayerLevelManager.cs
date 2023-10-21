using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelManager : MonoBehaviour
{
	public float currentExp = 0f;

	public int currentLevel = 1;

	public float nextLevelExpMultiplier;

	public float expToNextLevel = 50f;

	[SerializeField]
	private Transform skillButtonSpawner;

	[SerializeField]
	private GameObject skillButtonPF;

	[SerializeField]
	private GameObject skillSpawnManager;

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
			if (currentLevel == 2)
			{
				GameManager.Instance.SetGameState(GameState.UpgradeMenu);
				SpawnSkillButton();
			}
			currentExp -= expToNextLevel;
			expToNextLevel *= nextLevelExpMultiplier;
		}
		healthBar.transform.localScale = new Vector3(currentExp / expToNextLevel % 1f, 1f, 1f);
		levelTextUI.text = currentLevel.ToString();
	}

	public void SpawnSkillButton()
	{
		GameObject skillButton = Object.Instantiate(skillButtonPF, skillButtonSpawner.transform.position, Quaternion.identity);
		skillButton.transform.parent = skillButtonSpawner;
		skillButton.GetComponent<Button>().onClick.AddListener(delegate
		{
			skillSpawnManager.transform.GetComponent<SkillUpgradeManager>().TryUpgrade();
			GameManager.Instance.SetGameState(GameState.Playing);
			Object.Destroy(skillButton);
		});
	}
}
