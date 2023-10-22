using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Managers
{
	public class EnemySpawnManager : MonoBehaviour
	{
		[SerializeField]
		private GameObject[] enemyPrefab;

		[SerializeField]
		private Transform playerTransform;

		[SerializeField]
		private float spawnRadius = 5f;

		[SerializeField]
		private int numberOfEnemiesToSpawn = 3;

		[SerializeField]
		private float secondsTillNextWave = 5f;

		[SerializeField]
		private TextMeshProUGUI enemyCounterText;

		private int enemiesKilled = 0;

		private List<GameObject> spawnedEnemies = new List<GameObject>();

		private void Start()
		{
			InvokeRepeating(nameof(SpawnEnemies), 0f, secondsTillNextWave);
		}

		private void SpawnEnemies()
		{
			for (int i = 0; i < numberOfEnemiesToSpawn; i++)
			{
				if (playerTransform != null)
				{
					float f = UnityEngine.Random.Range(0f, MathF.PI * 2f);
					var playerPosition = playerTransform.position;
					float x = playerPosition.x + spawnRadius * Mathf.Cos(f);
					float y = playerPosition.y + spawnRadius * Mathf.Sin(f);
					Vector3 position = new Vector3(x, y, 0f);
					int num = UnityEngine.Random.Range(0, enemyPrefab.Length);
					GameObject item = Instantiate(enemyPrefab[num], position, Quaternion.identity);
					spawnedEnemies.Add(item);
				}
			}
		}

		public List<GameObject> GetSpawnedEnemies()
		{
			return spawnedEnemies;
		}

		private void RemoveEnemy(GameObject enemy)
		{
			for (int i = 0; i < spawnedEnemies.Count; i++)
			{
				if (spawnedEnemies[i] != null && spawnedEnemies[i] == enemy)
				{
					spawnedEnemies[i] = null;
					break;
				}
			}
		}

		public void EnemyKilled(GameObject enemy)
		{
			IncrementEnemiesKilled();
			RemoveEnemy(enemy);
			UnityEngine.Object.Destroy(enemy);
		}

		private void IncrementEnemiesKilled()
		{
			enemiesKilled++;
			UpdateUI();
		}

		private void UpdateUI()
		{
			enemyCounterText.text = enemiesKilled.ToString();
		}
	}
}
