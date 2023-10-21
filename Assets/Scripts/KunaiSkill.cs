using System;
using UnityEngine;

public class KunaiSkill : BaseSkill<SkillSO>
{
	[SerializeField]
	private GameObject projectilePrefab;

	[SerializeField]
	private float shootingCooldown = 1f;

	[SerializeField]
	private EnemySpawnManager spawnManager;

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
		if (!(spawnManager != null))
		{
			return;
		}
		foreach (GameObject spawnedEnemy in spawnManager.GetSpawnedEnemies())
		{
			if (spawnedEnemy != null)
			{
				float num2 = Vector2.Distance(base.transform.position, spawnedEnemy.transform.position);
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
		GameObject gameObject = UnityEngine.Object.Instantiate(projectilePrefab, base.transform.position, Quaternion.identity);
		gameObject.GetComponent<Projectile>().Launch(closestEnemy);
		lastShotTime = Time.time;
	}

	public override void setScriptableObjectProperties(SkillSO sOType)
	{
		throw new NotImplementedException();
	}
}
