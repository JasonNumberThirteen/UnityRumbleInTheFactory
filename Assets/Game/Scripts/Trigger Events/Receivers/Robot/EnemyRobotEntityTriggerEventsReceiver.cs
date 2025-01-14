using UnityEngine;

[RequireComponent(typeof(EnemyRobotEntity))]
public class EnemyRobotEntityTriggerEventsReceiver : RobotEntityTriggerEventsReceiver
{
	private EnemyRobotEntity enemyRobotEntity;
	private BonusSpawnManager bonusSpawnManager;

	public override void TriggerOnEnter(GameObject sender)
	{
		if(enemyRobotEntity.HasBonus && bonusSpawnManager != null)
		{
			bonusSpawnManager.SpawnRandomBonus();
			enemyRobotEntity.RemoveBonusTypeProperties();
		}

		base.TriggerOnEnter(sender);
	}
	
	protected override void Awake()
	{
		base.Awake();

		enemyRobotEntity = GetComponent<EnemyRobotEntity>();
		bonusSpawnManager = ObjectMethods.FindComponentOfType<BonusSpawnManager>();
	}
}