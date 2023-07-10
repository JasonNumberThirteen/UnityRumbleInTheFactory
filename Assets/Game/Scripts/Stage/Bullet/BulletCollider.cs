using UnityEngine;

public class BulletCollider : MonoBehaviour
{
	public GameObject splatterEffect;
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		Instantiate(splatterEffect, gameObject.transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}