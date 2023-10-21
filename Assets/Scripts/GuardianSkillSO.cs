using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/Guardian")]
public class GuardianSkillSO : SkillSO
{
	public int numberOfBalls;

	public float rotationSpeed;

	public float distanceFromPlayer;

	public float rotationSelfSpeed;

	public float knockBack;

	public Sprite ballSprite;
}
