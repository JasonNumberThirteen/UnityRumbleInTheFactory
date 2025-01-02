using UnityEngine;

public class FreezeBonusTrigger : TimedBonusTrigger
{
	private RobotDisablingManager enemyFreezeManager;
	
	public override void TriggerOnEnter(GameObject sender)
	{
		if(enemyFreezeManager != null)
		{
			enemyFreezeManager.InitiateFreeze(GetDuration());
		}

		base.TriggerOnEnter(sender);
	}

	private void Awake()
	{
		enemyFreezeManager = FindAnyObjectByType<RobotDisablingManager>(FindObjectsInactive.Include);
	}
}