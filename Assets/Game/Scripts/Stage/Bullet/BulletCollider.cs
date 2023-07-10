using UnityEngine;

public class BulletCollider : MonoBehaviour
{
	public GameObject splatterEffect;
	public string[] excludedTags;
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		foreach (string et in excludedTags)
		{
			if(collider.CompareTag(et))
			{
				return;
			}
		}
		
		Instantiate(splatterEffect, gameObject.transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}