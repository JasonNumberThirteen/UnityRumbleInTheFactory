using UnityEngine;

public class BulletEntityTriggerEventsSender : MonoBehaviour
{
	[SerializeField] private GameObject splatterEffectPrefab;
	[SerializeField] private string[] ignoredTags;

	private StageEventsManager stageEventsManager;
	private bool triggered;

	private void Awake()
	{
		stageEventsManager = ObjectMethods.FindComponentOfType<StageEventsManager>();
	}
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(triggered)
		{
			return;
		}
		
		foreach (var ignoredTag in ignoredTags)
		{
			if(collider.CompareTag(ignoredTag))
			{
				return;
			}
		}

		triggered = true;

		SendTriggerOnEnter(collider);
		SpawnSplatterEffect();
		SendEvent();
		Destroy(gameObject);
	}

	private void SendTriggerOnEnter(Collider2D collider)
	{
		if(collider.TryGetComponent(out ITriggerableOnEnter triggerableOnEnter))
		{
			triggerableOnEnter.TriggerOnEnter(gameObject);
		}
	}

	private void SpawnSplatterEffect()
	{
		if(splatterEffectPrefab != null)
		{
			Instantiate(splatterEffectPrefab, transform.position, Quaternion.identity);
		}
	}

	private void SendEvent()
	{
		if(stageEventsManager != null)
		{
			stageEventsManager.SendEvent(StageEventType.BulletDestroyed, gameObject);
		}
	}
}