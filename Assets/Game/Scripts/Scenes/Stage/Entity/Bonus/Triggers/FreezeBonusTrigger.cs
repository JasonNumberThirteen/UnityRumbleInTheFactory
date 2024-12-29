using UnityEngine;

public class FreezeBonusTrigger : TimedBonusTrigger
{
	private EnemyFreezeManager enemyFreezeManager;
	
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
		enemyFreezeManager = FindAnyObjectByType<EnemyFreezeManager>(FindObjectsInactive.Include);
	}
}