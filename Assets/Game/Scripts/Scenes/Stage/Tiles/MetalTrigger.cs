using UnityEngine;

public class MetalTrigger : MonoBehaviour, ITriggerable
{
	public void TriggerEffect(GameObject sender)
	{
		BulletStats bs = sender.GetComponent<BulletStats>();

		if(bs != null && bs.canDestroyMetal)
		{
			Destroy(gameObject);
		}
	}
}