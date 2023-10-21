using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[SerializeField]
	private PlayerController player;

	public GameState currentGameState;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Object.Destroy(base.gameObject);
		}
		currentGameState = GameState.Playing;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Update()
	{
		switch (currentGameState)
		{
		case GameState.Playing:
			UnpauseGame();
			break;
		case GameState.Paused:
			PauseGame();
			break;
		case GameState.UpgradeMenu:
			PauseGame();
			break;
		}
	}

	public void SetGameState(GameState gameState)
	{
		currentGameState = gameState;
	}

	public GameState GetGameState()
	{
		return currentGameState;
	}

	public PlayerController GetPlayer()
	{
		return player;
	}

	private void PauseGame()
	{
		Time.timeScale = 0f;
	}

	private void UnpauseGame()
	{
		Time.timeScale = 1f;
	}
}
