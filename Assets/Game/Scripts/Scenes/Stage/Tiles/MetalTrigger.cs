using UnityEngine;

public class MetalTrigger : MonoBehaviour, ITriggerable
{
	public void TriggerEffect(GameObject sender)
	{
		if(CanBeDestroyedByBullet(sender))
		{
			Destroy(gameObject);
		}
	}

	private bool CanBeDestroyedByBullet(GameObject sender) => sender.TryGetComponent(out BulletStats bs) && bs.canDestroyMetal;
}