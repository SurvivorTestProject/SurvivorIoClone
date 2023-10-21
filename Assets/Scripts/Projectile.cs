using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float speed = 10f;

	public float lifetime = 6f;

	private Vector3 direction;

	[SerializeField]
	private float damage;

	private Transform target;

	private void Start()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
	}

	private void Update()
	{
		base.transform.position += direction * speed * Time.deltaTime;
		if ((bool)target)
		{
			ShootTowardsTarget();
		}
		Rigidbody2D component = GetComponent<Rigidbody2D>();
		if ((bool)component && ((double)component.velocity.x > 0.3 || (double)component.velocity.y > 0.3))
		{
			base.transform.LookAt(base.transform.position + new Vector3(component.velocity.x, component.velocity.x, 0f));
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			Enemy component = collision.gameObject.GetComponent<Enemy>();
			if (component != null)
			{
				component.TakeDamage(damage);
			}
			Object.Destroy(base.gameObject);
		}
	}

	public void Launch(Transform closestEnemy)
	{
		target = closestEnemy;
		Object.Destroy(base.gameObject, lifetime);
	}

	public void ShootTowardsTarget()
	{
		Vector3 vector = (direction = (target.position - base.transform.position).normalized);
		base.transform.rotation = RotateTowards(vector);
	}

	public Quaternion RotateTowards(Vector3 direction)
	{
		float angle = Mathf.Atan2(direction.y, direction.x) * 57.29578f;
		return Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
