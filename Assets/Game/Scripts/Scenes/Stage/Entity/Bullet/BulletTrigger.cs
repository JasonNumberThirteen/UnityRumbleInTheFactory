using UnityEngine;

public class BulletTrigger : MonoBehaviour
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

		ITriggerable triggerable = collider.gameObject.GetComponent<ITriggerable>();

		if(triggerable != null)
		{
			triggerable.TriggerEffect(gameObject);
		}
		
		Instantiate(splatterEffect, gameObject.transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}