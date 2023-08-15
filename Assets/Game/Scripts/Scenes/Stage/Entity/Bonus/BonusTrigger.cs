using UnityEngine;

public abstract class BonusTrigger : MonoBehaviour, ITriggerable
{
	public virtual void TriggerEffect(GameObject sender)
	{
		StageManager.instance.AddPoints(gameObject, StageManager.instance.pointsForBonus);
		Destroy(gameObject);
	}
}