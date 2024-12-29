using UnityEngine;

public abstract class BonusTrigger : MonoBehaviour, ITriggerableOnEnter
{
	public virtual void TriggerOnEnter(GameObject sender)
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