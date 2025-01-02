using UnityEngine;

public class BonusEnemyRobotTrigger : RobotTriggerEventsReceiver
{
	private BonusSpawnManager bonusSpawnManager;
	
	public override void TriggerOnEnter(GameObject sender)
	{
		if(bonusSpawnManager != null)
		{
			bonusSpawnManager.InstantiateRandomBonus();
		}

		if(TryGetComponent(out BonusEnemyRobotColor berc))
		{
			berc.RestoreInitialColor();
			Destroy(berc);
		}

		base.TriggerOnEnter(sender);
	}

	protected override void Awake()
	{
		base.Awake();

		bonusSpawnManager = FindAnyObjectByType<BonusSpawnManager>();
	}
}