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
		StageManager sm = StageManager.instance;
		
		if(sender.TryGetComponent(out PlayerRobotData prd) && !sm.stateManager.GameIsOver())
		{
			sm.AddPoints(gameObject, prd.Data, sm.pointsForBonus);
			sm.audioManager.PlayBonusCollectSound();
		}
	}
}