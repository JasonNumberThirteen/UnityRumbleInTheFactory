using UnityEngine;

public class MetalTileTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter
{
	public void TriggerOnEnter(GameObject sender)
	{
		if(CanBeDestroyedByBullet(sender))
		{
			Destroy(gameObject);
		}
	}

	private bool CanBeDestroyedByBullet(GameObject sender) => sender.TryGetComponent(out BulletEntity bulletEntity) && bulletEntity.CanDestroyMetal();
}