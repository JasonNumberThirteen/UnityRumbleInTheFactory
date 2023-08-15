using UnityEngine;

public class EnemyRobotHealth : RobotHealth
{
	public EnemyData data;
	public PlayerData playerData;
	
	protected override void Die(GameObject sender)
	{
		++StageManager.instance.DefeatedEnemies;

		playerData.AddDefeatedEnemy(data);
		StageManager.instance.AddPoints(gameObject, data.score);
		base.Die(sender);
	}
}