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

	private List<GameObject> balls = new List<GameObject>();

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
			guardianSprite.Rotate(Vector3.forward * rotationSelfSpeed * Time.deltaTime);
		}
	}

	public override void setScriptableObjectProperties(GuardianSkillSO receivedGuardianSkillSO)
	{
		damage = receivedGuardianSkillSO.damage;
		numberOfBalls = receivedGuardianSkillSO.numberOfBalls;
		rotationSpeed = receivedGuardianSkillSO.rotationSpeed;
		distanceFromPlayer = receivedGuardianSkillSO.distanceFromPlayer;
		rotationSelfSpeed = receivedGuardianSkillSO.rotationSelfSpeed;
		knockBack = receivedGuardianSkillSO.knockBack;
	}

	private void InstantiateNewBall(int ballIndex)
	{
		if (ballPrefab != null && skillSpawner != null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(ballPrefab, skillSpawner.transform.position, Quaternion.identity);
			gameObject.transform.parent = base.transform;
			balls.Add(gameObject);
			GuardianSkillInstance component = gameObject.GetComponent<GuardianSkillInstance>();
			component.setKnockback(knockBack);
			component.setDamage(damage);
			component.setRotationSelfSpeed(rotationSelfSpeed);
		}
		if (skillSpawner != null)
		{
			Debug.Log("No apwaner");
		}
		if (ballPrefab != null)
		{
			Debug.Log("No ball prefab");
		}
	}

	private void CreateBalls()
	{
		if (ballPrefab != null)
		{
			for (int i = 0; i < numberOfBalls; i++)
			{
				InstantiateNewBall(i);
			}
		}
	}

	private void TranslateBalls()
	{
		for (int i = 0; i < numberOfBalls; i++)
		{
			float z = (float)i * (360f / (float)numberOfBalls);
			Vector3 localPosition = Quaternion.Euler(0f, 0f, z) * Vector3.right * distanceFromPlayer;
			balls[i].transform.localPosition = localPosition;
		}
	}

	private void RotateAroundPoint(Transform point, Transform center, float angle)
	{
		angle *= MathF.PI / 180f;
		float x = Mathf.Cos(angle) * (point.position.x - center.position.x) - Mathf.Sin(angle) * (point.position.y - center.position.y) + center.position.x;
		float y = Mathf.Sin(angle) * (point.position.x - center.position.x) + Mathf.Cos(angle) * (point.position.y - center.position.y) + center.position.y;
		point.transform.position = new Vector3(x, y, 0f);
	}

	private void RotateBalls()
	{
		base.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
	}

	private void IncrementBalls()
	{
		numberOfBalls++;
		for (int i = 0; i < numberOfBalls; i++)
		{
			if (i == numberOfBalls - 1)
			{
				InstantiateNewBall(i);
			}
			else
			{
				balls[i].transform.position = skillSpawner.transform.position;
			}
		}
	}
}
