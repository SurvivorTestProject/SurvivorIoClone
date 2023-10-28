using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Skills
{
	public class GuardianSkill : BaseSkill
	{
		[SerializeField]
		private GameObject bladePrefab;
		private float rotationSpeed;
		private float distanceFromPlayer;
		private float rotationSelfSpeed;
		private float knockBack;
		private Transform guardianSprite;
		private List<GameObject> blades = new List<GameObject>();
		public override void Awake()
		{
			base.Awake();
			setStats();
			guardianSprite = bladePrefab.transform.GetChild(0);
		}

		private void Start()
		{
			if (bladePrefab != null)
			{
				CreateBalls();
			}
		}

		public override void setStats()
		{
			
			base.setStats();
			rotationSpeed = stats.GetStatFloat(StatType.rotationSpeed);
			console.log("Set stats" + rotationSpeed);
			damage = stats.GetStatFloat(StatType.damage);
			int receivedNumberOfProjectiles = (int)stats.GetStatFloat(StatType.numberOfProjectiles);
			for(int i = 0;i<receivedNumberOfProjectiles ; i++)
			{
				IncrementBlades();
			}

			numberOfProjectiles = receivedNumberOfProjectiles;
			distanceFromPlayer = stats.GetStatFloat(StatType.distanceFromPlayer);
			rotationSelfSpeed = stats.GetStatFloat(StatType.rotationChildSpeed);
			knockBack = stats.GetStatFloat(StatType.knockBack);
		}
		
		private void Update()
		{
			if (skillSpawner != null)
			{
				RotateBlades();
				TranslateBalls();
			}
			if ((bool)guardianSprite)
			{
				guardianSprite.Rotate(Vector3.forward * (rotationSelfSpeed * Time.deltaTime));
			}
		}

		public override void setScriptableObjectProperties(SkillSO receivedGuardianSkillSo)
		{
			damage = receivedGuardianSkillSo.damage;
			numberOfProjectiles = receivedGuardianSkillSo.numberOfProjectiles;
			distanceFromPlayer = receivedGuardianSkillSo.distanceFromPlayer;
			rotationSelfSpeed = receivedGuardianSkillSo.rotationChildSpeed;
			knockBack = receivedGuardianSkillSo.knockBack;
			if (receivedGuardianSkillSo.numberOfProjectiles > 2)
			{
				InstantiateNewBall();
			}
		}

		private void InstantiateNewBall()
		{
			if (bladePrefab != null && skillSpawner != null)
			{
				GameObject bladeGO = Instantiate(bladePrefab, skillSpawner.transform.position, Quaternion.identity);
				bladeGO.transform.parent = transform;
				blades.Add(bladeGO);
				GuardianSkillInstance component = bladeGO.GetComponent<GuardianSkillInstance>();
				component.setKnockback(knockBack);
				component.setDamage(damage);
				component.setRotationSelfSpeed(rotationSelfSpeed);
			}
		}

		private void CreateBalls()
		{
			if (bladePrefab != null)
			{
				for (int i = 0; i < numberOfProjectiles; i++)
				{
					InstantiateNewBall();
				}
			}
		}

		private void TranslateBalls()
		{
			for (int i = 0; i < numberOfProjectiles; i++)
			{
				float z = i * (360f / numberOfProjectiles);
				Vector3 localPosition = Quaternion.Euler(0f, 0f, z) * Vector3.right * distanceFromPlayer;
				blades[i].transform.localPosition = localPosition;
			}
		}
		private void RotateBlades()
		{
			transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
		}

		private void IncrementBlades()
		{
			numberOfProjectiles++;
			for (int i = 0; i < numberOfProjectiles; i++)
			{
				if (i == numberOfProjectiles - 1)
				{
					InstantiateNewBall();
				}
				else
				{
					blades[i].transform.position = skillSpawner.transform.position;
				}
			}
		}
	}
}
