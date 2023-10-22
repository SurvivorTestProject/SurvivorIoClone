using Managers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private Rigidbody2D _rigidBody;

	[SerializeField]
	private FloatingJoystick _joystick;

	[SerializeField]
	private float _moveSpeed;

	[SerializeField]
	private EnemySpawnManager spawnManager;

	private Transform closestEnemy;


	private Transform playerSprite;

	private void Awake()
	{
		playerSprite = base.transform.GetChild(0);
	}

	private void FixedUpdate()
	{
		MovePlayer();
	}

	private void MovePlayer()
	{
		_rigidBody.velocity = new Vector2(_joystick.Horizontal * _moveSpeed, _joystick.Vertical * _moveSpeed);
		if ((bool)playerSprite)
		{
			if (_joystick.Horizontal > 0f)
			{
				playerSprite.localRotation = Quaternion.Euler(0f, 0f, 0f);
			}
			if (_joystick.Horizontal < 0f)
			{
				playerSprite.localRotation = Quaternion.Euler(0f, 180f, 0f);
			}
		}
	}

	private void FindClosestEnemy()
	{
		float num = float.PositiveInfinity;
		closestEnemy = null;
		if (!(spawnManager != null))
		{
			return;
		}
		foreach (GameObject spawnedEnemy in spawnManager.GetSpawnedEnemies())
		{
			if (spawnedEnemy != null)
			{
				float num2 = Vector2.Distance(base.transform.position, spawnedEnemy.transform.position);
				if (num2 < num)
				{
					num = num2;
					closestEnemy = spawnedEnemy.transform;
				}
			}
		}
	}
}
