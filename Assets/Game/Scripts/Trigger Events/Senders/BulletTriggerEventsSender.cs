using UnityEngine;

public class BulletTriggerEventsSender : MonoBehaviour
{
	[SerializeField] private GameObject splatterPrefab;
	[SerializeField] private string[] ignoredTags;
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		foreach (var ignoredTag in ignoredTags)
		{
			if(collider.CompareTag(ignoredTag))
			{
				return;
			}
		}

		SendTriggerOnEnter(collider);
		SpawnSplatterIfPossible();
		Destroy(gameObject);
	}

	private void SendTriggerOnEnter(Collider2D collider)
	{
		if(collider.TryGetComponent(out ITriggerableOnEnter triggerableOnEnter))
		{
			triggerableOnEnter.TriggerOnEnter(gameObject);
		}
	}

	private void SpawnSplatterIfPossible()
	{
		if(splatterPrefab != null)
		{
			Instantiate(splatterPrefab, transform.position, Quaternion.identity);
		}
	}
}