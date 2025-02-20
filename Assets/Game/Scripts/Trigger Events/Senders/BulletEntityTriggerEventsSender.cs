using UnityEngine;

[RequireComponent(typeof(BulletEntity))]
public class BulletEntityTriggerEventsSender : MonoBehaviour
{
	[SerializeField] private GameObject splatterEffectPrefab;
	[SerializeField] private string[] ignoredTags;

	private BulletEntity bulletEntity;
	private StageEventsManager stageEventsManager;
	private bool triggered;

	protected virtual void OnTriggerEnter2D(Collider2D collider)
	{
		if(triggered)
		{
			return;
		}

		if(collider.TryGetComponent(out BulletEntity bulletEntity) && this.bulletEntity.GetParentGO() == bulletEntity.GetParentGO())
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

	private void Awake()
	{
		bulletEntity = GetComponent<BulletEntity>();
		stageEventsManager = ObjectMethods.FindComponentOfType<StageEventsManager>();
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
			stageEventsManager.SendEvent(StageEventType.BulletWasDestroyed, gameObject);
		}
	}
}