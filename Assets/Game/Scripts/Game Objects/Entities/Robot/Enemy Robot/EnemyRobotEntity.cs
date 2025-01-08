using UnityEngine;

[RequireComponent(typeof(EnemyRobotEntityHealth))]
public class EnemyRobotEntity : RobotEntity
{
	private EnemyRobotEntityHealth enemyRobotEntityHealth;
	private EnemyRobotEntitySpawnManager enemyRobotEntitySpawnManager;
	
	public override bool IsFriendly() => false;

	public override void OnLifeBonusCollected(int lives)
	{
		enemyRobotEntityHealth.IncreaseHealthBy(lives);
	}

	protected override void Awake()
	{
		base.Awake();

		enemyRobotEntityHealth = GetComponent<EnemyRobotEntityHealth>();
		enemyRobotEntitySpawnManager = FindAnyObjectByType<EnemyRobotEntitySpawnManager>(FindObjectsInactive.Include);
	}

	private void OnDestroy()
	{
		if(enemyRobotEntitySpawnManager != null)
		{
			enemyRobotEntitySpawnManager.CountDefeatedEnemy();
		}
	}
}