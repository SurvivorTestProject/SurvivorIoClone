using System;
using Managers;
using UnityEngine;

namespace Skills
{
	public class KunaiSkill : BaseSkill
	{
		[SerializeField]
		private GameObject projectilePrefab;

		[SerializeField]
		private float shootingCooldown = 1f;

		[SerializeField]
		private EnemySpawnManager enemySpawnManager;

		private Transform closestEnemy;

		private float lastShotTime;

		private void Update()
		{
			if (CanShoot())
			{
				FindClosestEnemy();
				if (closestEnemy != null)
				{
					Shoot();
				}
			}
		}

		private void FindClosestEnemy()
		{
			float num = float.PositiveInfinity;
			closestEnemy = null;

			if (!(enemySpawnManager != null))
			{
				return;
			}
			foreach (GameObject spawnedEnemy in enemySpawnManager.GetSpawnedEnemies())
			{
				if (spawnedEnemy != null)
				{
					float num2 = Vector2.Distance(transform.position, spawnedEnemy.transform.position);
					if (num2 < num)
					{
						num = num2;
						closestEnemy = spawnedEnemy.transform;
					}
				}
			}
		}

		private bool CanShoot()
		{
			return Time.time - lastShotTime >= shootingCooldown;
		}

		private void Shoot()
		{
			GameObject gameObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
			gameObject.GetComponent<Projectile>().Launch(closestEnemy);
			lastShotTime = Time.time;
		}

		public override void setScriptableObjectProperties(SkillSO sOType)
		{
			throw new NotImplementedException();
		}
	}
}
