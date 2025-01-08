using UnityEngine;

public class BonusEnemyRobotEntityTriggerEventsReceiver : RobotEntityTriggerEventsReceiver
{
	private BonusSpawnManager bonusSpawnManager;
	
	public override void TriggerOnEnter(GameObject sender)
	{
		if(TryGetComponent(out BonusEnemyRobotEntityRendererColorAdjuster bonusEnemyRobotEntityRendererColorAdjuster))
		{
			if(bonusSpawnManager != null)
			{
				bonusSpawnManager.SpawnRandomBonus();
			}
			
			bonusEnemyRobotEntityRendererColorAdjuster.RestoreInitialColor();
			Destroy(bonusEnemyRobotEntityRendererColorAdjuster);
		}

		base.TriggerOnEnter(sender);
	}

	protected override void Awake()
	{
		base.Awake();

		bonusSpawnManager = FindAnyObjectByType<BonusSpawnManager>();
	}
}