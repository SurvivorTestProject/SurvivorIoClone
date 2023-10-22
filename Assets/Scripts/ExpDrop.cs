using Managers;
using UnityEngine;

public class ExpDrop : MonoBehaviour
{
	[SerializeField]
	private int expValue;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag(ObjectTagType.Player))
		{
			collision.gameObject.GetComponent<PlayerLevelManager>().AddExp(expValue);
			Object.Destroy(base.gameObject);
		}
	}
}
