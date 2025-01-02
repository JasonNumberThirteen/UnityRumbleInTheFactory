using UnityEngine;

public class MetalTrigger : MonoBehaviour, ITriggerableOnEnter
{
	public void TriggerOnEnter(GameObject sender)
	{
		if(CanBeDestroyedByBullet(sender))
		{
			Destroy(gameObject);
		}
	}

	private bool CanBeDestroyedByBullet(GameObject sender) => sender.TryGetComponent(out Bullet bulletStats) && bulletStats.canDestroyMetal;
}