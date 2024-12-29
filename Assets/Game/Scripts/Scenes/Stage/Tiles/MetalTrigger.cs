using UnityEngine;

public class MetalTrigger : MonoBehaviour, ITriggerableOnEnter
{
	public void TriggerEffect(GameObject sender)
	{
		if(CanBeDestroyedByBullet(sender))
		{
			Destroy(gameObject);
		}
	}

	private bool CanBeDestroyedByBullet(GameObject sender) => sender.TryGetComponent(out BulletStats bulletStats) && bulletStats.canDestroyMetal;
}