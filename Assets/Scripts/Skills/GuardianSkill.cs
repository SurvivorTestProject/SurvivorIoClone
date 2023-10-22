using System;
using System.Collections.Generic;
using UnityEngine;

public class GuardianSkill : BaseSkill<GuardianSkillSO>
{
	[SerializeField]
	private GameObject ballPrefab;
	private int numberOfBalls;
	private float rotationSpeed;
	private float distanceFromPlayer;
	private float rotationSelfSpeed;
	private float knockBack;
	private Transform guardianSprite;
	private List<GameObject> blades = new List<GameObject>();
	
	public override void Awake()
	{
		base.Awake();
		guardianSprite = ballPrefab.transform.GetChild(0);
	}

	private void Start()
	{
		if (ballPrefab != null)
		{
			CreateBalls();
		}
	}

	private void Update()
	{
		if (skillSpawner != null)
		{
			RotateBalls();
			TranslateBalls();
		}
		if ((bool)guardianSprite)
		{
			guardianSprite.Rotate(Vector3.forward * (rotationSelfSpeed * Time.deltaTime));
		}
	}

	public override void setScriptableObjectProperties(GuardianSkillSO receivedGuardianSkillSo)
	{
		damage = receivedGuardianSkillSo.damage;
		numberOfBalls = receivedGuardianSkillSo.numberOfBalls;
		rotationSpeed = receivedGuardianSkillSo.rotationSpeed;
		distanceFromPlayer = receivedGuardianSkillSo.distanceFromPlayer;
		rotationSelfSpeed = receivedGuardianSkillSo.rotationSelfSpeed;
		knockBack = receivedGuardianSkillSo.knockBack;
		if (receivedGuardianSkillSo.numberOfBalls > 2)
		{
			InstantiateNewBall();
		}
	}

	private void InstantiateNewBall()
	{
		if (ballPrefab != null && skillSpawner != null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(ballPrefab, skillSpawner.transform.position, Quaternion.identity);
			gameObject.transform.parent = base.transform;
			blades.Add(gameObject);
			GuardianSkillInstance component = gameObject.GetComponent<GuardianSkillInstance>();
			component.setKnockback(knockBack);
			component.setDamage(damage);
			component.setRotationSelfSpeed(rotationSelfSpeed);
		}
	}

	private void CreateBalls()
	{
		if (ballPrefab != null)
		{
			for (int i = 0; i < numberOfBalls; i++)
			{
				InstantiateNewBall();
			}
		}
	}

	private void TranslateBalls()
	{
		for (int i = 0; i < numberOfBalls; i++)
		{
			float z = (float)i * (360f / (float)numberOfBalls);
			Vector3 localPosition = Quaternion.Euler(0f, 0f, z) * Vector3.right * distanceFromPlayer;
			blades[i].transform.localPosition = localPosition;
		}
	}

	private void RotateAroundPoint(Transform point, Transform center, float angle)
	{
		angle *= MathF.PI / 180f;
		var pointPosition = point.position;
		var centerPosition = center.position;
		float x = Mathf.Cos(angle) * (pointPosition.x - centerPosition.x) - Mathf.Sin(angle) * (pointPosition.y - centerPosition.y) + centerPosition.x;
		float y = Mathf.Sin(angle) * (pointPosition.x - centerPosition.x) + Mathf.Cos(angle) * (pointPosition.y - centerPosition.y) + centerPosition.y;
		point.transform.position = new Vector3(x, y, 0f);
	}

	private void RotateBalls()
	{
		base.transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
	}

	private void IncrementBlades()
	{
		numberOfBalls++;
		for (int i = 0; i < numberOfBalls; i++)
		{
			if (i == numberOfBalls - 1)
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
