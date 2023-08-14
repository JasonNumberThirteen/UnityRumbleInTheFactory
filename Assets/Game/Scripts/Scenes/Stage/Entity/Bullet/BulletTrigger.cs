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

		TriggerEffectOnCollider(collider);
		Instantiate(splatterEffect, gameObject.transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	private void TriggerEffectOnCollider(Collider2D collider)
	{
		if(collider.gameObject.TryGetComponent(out ITriggerable triggerable))
		{
			triggerable.TriggerEffect(gameObject);
		}
	}
}