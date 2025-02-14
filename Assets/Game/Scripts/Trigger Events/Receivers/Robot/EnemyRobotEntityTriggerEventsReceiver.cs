using UnityEngine;

[RequireComponent(typeof(EnemyRobotEntity))]
public class EnemyRobotEntityTriggerEventsReceiver : RobotEntityTriggerEventsReceiver
{
	private EnemyRobotEntity enemyRobotEntity;
	private BonusSpawnManager bonusSpawnManager;

	public override void TriggerOnEnter(GameObject sender)
	{
		if(sender.TryGetComponent(out BulletEntity _))
		{
			TriggerOnEnterWithBullet();
		}

		base.TriggerOnEnter(sender);
	}
	
	protected override void Awake()
	{
		base.Awake();

		enemyRobotEntity = GetComponent<EnemyRobotEntity>();
		bonusSpawnManager = ObjectMethods.FindComponentOfType<BonusSpawnManager>();
	}

	private void TriggerOnEnterWithBullet()
	{
		if(bonusSpawnManager == null || !enemyRobotEntity.HasBonus)
		{
			return;
		}
		
		bonusSpawnManager.SpawnRandomBonus();
		enemyRobotEntity.RemoveBonusTypeProperties();
	}
}