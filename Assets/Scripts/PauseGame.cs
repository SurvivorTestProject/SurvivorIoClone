using UnityEngine;

public class PauseGame : MonoBehaviour
{
	private bool isGamePaused = false;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ToggleGamePause()
	{
		isGamePaused = !isGamePaused;
		if (isGamePaused)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}
}
