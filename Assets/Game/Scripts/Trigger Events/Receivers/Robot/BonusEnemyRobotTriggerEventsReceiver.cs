using UnityEngine;

public class BonusEnemyRobotTriggerEventsReceiver : RobotEntityTriggerEventsReceiver
{
	private BonusSpawnManager bonusSpawnManager;
	
	public override void TriggerOnEnter(GameObject sender)
	{
		if(bonusSpawnManager != null)
		{
			bonusSpawnManager.InstantiateRandomBonus();
		}

		if(TryGetComponent(out BonusEnemyRobotColor bonusEnemyRobotColor))
		{
			bonusEnemyRobotColor.RestoreInitialColor();
			Destroy(bonusEnemyRobotColor);
		}

		base.TriggerOnEnter(sender);
	}

	protected override void Awake()
	{
		base.Awake();

		bonusSpawnManager = FindAnyObjectByType<BonusSpawnManager>();
	}
}