using UnityEngine;

public class EnemyRobotHealth : RobotHealth
{
	public EnemyData data;
	
	protected override void Die(GameObject sender)
	{
		++StageManager.instance.DefeatedEnemies;

		OnDefeatByPlayer(sender);
		base.Die(sender);
	}

	private void OnDefeatByPlayer(GameObject sender)
	{
		if(sender.TryGetComponent(out PlayerRobotData prd))
		{
			prd.Data.AddDefeatedEnemy(data);
			StageManager.instance.AddPoints(gameObject, prd.Data, data.score);
		}
	}
}