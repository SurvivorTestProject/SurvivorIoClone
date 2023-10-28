using Enums;
using StatsPackage;
using UnityEngine;

public abstract class BaseSkill: MonoBehaviour
{
	protected float damage;
	protected float skillCooldown;
	protected float flightSpeed;
	protected float lifetime;
	protected float range;
	protected int numberOfProjectiles;
	protected float projectileSize;
	private float rotationSpeed;

	public GameObject skillSpawner;
	[SerializeField] public Stats stats;
	
	public virtual void Awake()
	{
		skillSpawner = GameObject.FindGameObjectWithTag(ObjectTagType.SkillSpawnManager);
	}

	public abstract void setScriptableObjectProperties(SkillSO sOType);

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
	
	private void OnEnable()
	{
		stats.upgradeApplied += SetStatsOnUpgradeEvent;
	}

	private void OnDestroy()
	{
		stats.upgradeApplied -= SetStatsOnUpgradeEvent;
	}
	
	public virtual void setStats()
	{
		damage = stats.GetStatFloat(StatType.damage);
	}

	public void SetStatsOnUpgradeEvent(Stats _stats, StatsUpgrade _statsUpgrade)
	{
		setStats();
	}
}