using UnityEngine;

public class FreezeBonusTrigger : TimedBonusTrigger
{
	private EnemyFreezeManager enemyFreezeManager;
	
	public override void TriggerEffect(GameObject sender)
	{
		if(enemyFreezeManager != null)
		{
			enemyFreezeManager.InitiateFreeze(GetDuration());
		}

		base.TriggerEffect(sender);
	}

	private void Awake()
	{
		enemyFreezeManager = FindAnyObjectByType<EnemyFreezeManager>(FindObjectsInactive.Include);
	}
}