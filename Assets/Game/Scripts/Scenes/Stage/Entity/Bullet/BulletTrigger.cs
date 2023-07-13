using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
	public GameObject splatterEffect;
	public string[] excludedTags, triggerableTags;
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		foreach (string et in excludedTags)
		{
			if(collider.CompareTag(et))
			{
				return;
			}
		}

		foreach (string tt in triggerableTags)
		{
			if(collider.CompareTag(tt))
			{
				ITriggerable triggerable = collider.gameObject.GetComponent<ITriggerable>();

				if(triggerable != null)
				{
					triggerable.TriggerEffect();
				}
			}
		}
		
		Instantiate(splatterEffect, gameObject.transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}