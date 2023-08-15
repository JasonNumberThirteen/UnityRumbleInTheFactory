using UnityEngine;

public class EnemyRobotHealth : RobotHealth
{
	public EnemyData data;
	
	protected override void Die(GameObject sender)
	{
		++StageManager.instance.DefeatedEnemies;

		AddYourselfAsDefeatedByPlayer(sender);
		base.Die(sender);
	}

	private void AddYourselfAsDefeatedByPlayer(GameObject sender)
	{
		if(sender.TryGetComponent(out PlayerRobotData prd))
		{
			prd.Data.AddDefeatedEnemy(data);
			StageManager.instance.AddPoints(gameObject, prd.Data, data.score);
		}
	}
}