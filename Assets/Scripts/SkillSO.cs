using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/Basic Skill")]
public class SkillSO : ScriptableObject
{
	public new string name;

	public float attackSpeed;

	public float range;

	public float damage;

	public float skillCooldown;

	public float flightSpeed;
}
