using UnityEngine;

public class BonusEnemyRobotEntityTriggerEventsReceiver : RobotEntityTriggerEventsReceiver
{
	private BonusSpawnManager bonusSpawnManager;
	
	public override void TriggerOnEnter(GameObject sender)
	{
		if(TryGetComponent(out BonusEnemyRobotColor bonusEnemyRobotColor))
		{
			if(bonusSpawnManager != null)
			{
				bonusSpawnManager.InstantiateRandomBonus();
			}
			
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