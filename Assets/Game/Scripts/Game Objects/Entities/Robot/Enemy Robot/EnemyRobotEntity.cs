using UnityEngine;

[RequireComponent(typeof(EnemyRobotEntityHealth))]
public class EnemyRobotEntity : RobotEntity
{
	private EnemyRobotEntityHealth enemyRobotEntityHealth;
	
	public override bool IsFriendly() => false;

	public override void OnLifeBonusCollected(int lives)
	{
		enemyRobotEntityHealth.IncreaseHealthBy(lives);
	}

	protected override void Awake()
	{
		base.Awake();

		enemyRobotEntityHealth = GetComponent<EnemyRobotEntityHealth>();
	}

	private void OnDestroy()
	{
		StageManager.instance.CountDefeatedEnemy();
	}
}