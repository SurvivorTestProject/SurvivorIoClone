using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/Basic Skill")]
public class SkillSO : ScriptableObject
{
	public new string name;
	public string description;
	public float attackSpeed;
	public float range;
	public float damage;
	public float skillCooldown;
	public float flightSpeed;
	public int numberOfProjectiles;
	public float projectileSize;
	public float knockBack;
	public Sprite upgradeButtonSprite;
	public float rotationSpeed;
	public float distanceFromPlayer;
	public float rotationChildSpeed;
	public Sprite childSprite;
	public Sprite parentSprite;
}
