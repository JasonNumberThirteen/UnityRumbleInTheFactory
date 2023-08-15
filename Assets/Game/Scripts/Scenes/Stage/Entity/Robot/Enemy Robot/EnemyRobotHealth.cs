using UnityEngine;

public class EnemyRobotHealth : RobotHealth
{
	public EnemyData data;
	
	protected override void Die(GameObject sender)
	{
		++StageManager.instance.DefeatedEnemies;

		AddYourselfAsDefeatedByPlayer(sender);
		StageManager.instance.AddPoints(gameObject, data.score);
		base.Die(sender);
	}

	private void AddYourselfAsDefeatedByPlayer(GameObject sender)
	{
		if(sender.TryGetComponent(out PlayerRobotData prd))
		{
			prd.Data.AddDefeatedEnemy(data);
		}
	}
}