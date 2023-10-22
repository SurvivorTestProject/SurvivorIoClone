using UnityEngine;

public class GuardianSkillInstance : MonoBehaviour
{
	private float knockBack;

	private float damage;

	private GameObject player;

	private float rotationSelfSpeed;

	private Transform child;

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag(ObjectTagType.SkillSpawnManager);
		child = base.transform.Find("GuardianSprite").transform;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			PushEnemy(collision);
		}
	}

	private void Update()
	{
		if (child != null)
		{
			child.Rotate(Vector3.forward * rotationSelfSpeed * Time.deltaTime);
		}
	}

	public void setKnockback(float knockBackValue)
	{
		knockBack = knockBackValue;
	}

	public void setDamage(float damageValue)
	{
		damage = damageValue;
	}

	public void setRotationSelfSpeed(float value)
	{
		rotationSelfSpeed = value;
	}

	private void PushEnemy(Collider2D collision)
	{
		Enemy component = collision.gameObject.GetComponent<Enemy>();
		if (component != null)
		{
			component.TakeDamage(damage);
			Vector3 normalized = (component.transform.position - player.transform.position).normalized;
			Rigidbody2D component2 = collision.gameObject.GetComponent<Rigidbody2D>();
			if (component2 != null)
			{
				component2.AddForce(knockBack * normalized, ForceMode2D.Impulse);
			}
		}
	}
}
