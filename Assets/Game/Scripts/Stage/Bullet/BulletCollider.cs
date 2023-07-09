using UnityEngine;

public class BulletCollider : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collider)
	{
		Destroy(gameObject);
	}
}