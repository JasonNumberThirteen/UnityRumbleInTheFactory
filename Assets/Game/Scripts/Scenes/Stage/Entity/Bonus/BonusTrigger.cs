using UnityEngine;

public abstract class BonusTrigger : MonoBehaviour, ITriggerable
{
	public virtual void TriggerEffect(GameObject sender)
	{
		AddPointsToPlayer(sender);
		Destroy(gameObject);
	}

	private void AddPointsToPlayer(GameObject sender)
	{
		if(sender.TryGetComponent(out PlayerRobotData prd))
		{
			StageManager.instance.AddPoints(gameObject, prd.Data, StageManager.instance.pointsForBonus);
		}
	}
}