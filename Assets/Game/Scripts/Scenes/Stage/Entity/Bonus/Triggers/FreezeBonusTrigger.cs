using UnityEngine;

public class FreezeBonusTrigger : TimedBonusTrigger
{
	public override void TriggerEffect(GameObject sender)
	{
		StageManager.instance.enemyFreezeManager.InitiateFreeze(duration);
		base.TriggerEffect(sender);
	}
}