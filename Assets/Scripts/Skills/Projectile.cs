using Managers;
using UnityEngine;

namespace Skills
{
	public class Projectile : MonoBehaviour
	{
		public float speed = 10f;
		public float lifetime = 20f;
		private Vector3 direction;
		[SerializeField] private float damage;
		private Transform target;

		private void Start()
		{
			PlayerController player = GameManager.Instance.GetPlayer();
			Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}

		private void Update()
		{
			transform.position += direction * (speed * Time.deltaTime);
			if (target!=null)
			{
				ShootTowardsTarget();
				Rigidbody2D component = GetComponent<Rigidbody2D>();
				if (component && (component.velocity.x > 0.3 || component.velocity.y > 0.3))
				{
					Vector3 velocity = component.velocity;
					transform.LookAt(transform.position + new Vector3(velocity.x, velocity.x, 0f));
				}
			}

		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag(ObjectTagType.Enemy))
			{
				Enemy component = collision.gameObject.GetComponent<Enemy>();
				if (component != null)
				{
					component.TakeDamage(damage);
				}
				Destroy(gameObject);
			}
		}

		public void Launch(Transform closestEnemy)
		{
			target = closestEnemy;
			Destroy(gameObject, lifetime);
		}

		private void ShootTowardsTarget()
		{
			Vector3 vector = (direction = (target.position - transform.position).normalized);
			transform.rotation = RotateTowards(vector);
		}

		private Quaternion RotateTowards(Vector3 enemyDirection)
		{
			float angle = Mathf.Atan2(enemyDirection.y, enemyDirection.x) * 57.29578f;
			return Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}
}
