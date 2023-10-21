using UnityEngine;

public abstract class BaseSkill<SOType> : MonoBehaviour
{
	protected float damage;

	protected float skillCooldown;

	protected float flightSpeed;

	protected float lifetime;

	protected float range;

	public GameObject skillSpawner;

	public virtual void Awake()
	{
		skillSpawner = GameObject.FindGameObjectWithTag(ObjectTagType.SkillSpawnManager);
	}

	public abstract void setScriptableObjectProperties(SOType sOType);

	public virtual void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			damageEnemy(collision);
		}
	}

	public void damageEnemy(Collision2D collision)
	{
		Enemy component = collision.gameObject.GetComponent<Enemy>();
		if (component != null)
		{
			component.TakeDamage(damage);
		}
	}
}
