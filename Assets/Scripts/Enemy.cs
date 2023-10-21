using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[Serializable]
	public struct DropChanceItem
	{
		public GameObject expDropPF;

		public float expDropChance;
	}

	[SerializeField]
	private float health = 2f;

	[SerializeField]
	private GameObject expDropPF;

	[SerializeField]
	private float expDropChance;

	[SerializeField]
	private float moveSpeed = 3f;

	[SerializeField]
	private AnimationClip deathAnimation;

	private EnemySpawnManager spawnManager;

	private Transform player;

	private float deathAnimationDelay = 0.5f;

	public List<DropChanceItem> dropsList;

	private void Awake()
	{
		player = GameManager.Instance.GetPlayer().transform;
	}

	private void Start()
	{
		spawnManager = GameObject.FindGameObjectWithTag(ObjectTagType.EnemySpawnManager).GetComponent<EnemySpawnManager>();
	}

	private void Update()
	{
		if (player != null)
		{
			Vector2 vector = player.position - base.transform.position;
			vector.Normalize();
			MoveSprite(vector);
			base.transform.Translate(vector * moveSpeed * Time.deltaTime);
		}
	}

	private void MoveSprite(Vector2 direction)
	{
		SpriteRenderer component = GetComponent<SpriteRenderer>();
		if ((bool)component)
		{
			if (direction.x > 0f)
			{
				component.flipX = false;
			}
			if (direction.x < 0f)
			{
				component.flipX = true;
			}
		}
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health < 0f)
		{
			KillEnemy();
		}
	}

	private void KillEnemy()
	{
		StartCoroutine(KillEnemyEnumerator());
	}

	public void TrySpawnExp()
	{
		float num = UnityEngine.Random.Range(1, 101);
		float num2 = 0f;
		float num3 = 0f;
		foreach (DropChanceItem drops in dropsList)
		{
			num2 += drops.expDropChance;
			if (num < num2 && num > num3)
			{
				UnityEngine.Object.Instantiate(drops.expDropPF, base.transform.position, Quaternion.identity);
				break;
			}
			num3 = num2;
		}
	}

	private void PlayDeathAnimation()
	{
		Animator component = GetComponent<Animator>();
		component.Play(deathAnimation.name);
	}

	private IEnumerator KillEnemyEnumerator()
	{
		PlayDeathAnimation();
		GetComponent<Collider2D>().enabled = false;
		moveSpeed = 0f;
		yield return new WaitForSeconds(deathAnimationDelay);
		spawnManager.EnemyKilled(base.gameObject);
		TrySpawnExp();
	}
}
