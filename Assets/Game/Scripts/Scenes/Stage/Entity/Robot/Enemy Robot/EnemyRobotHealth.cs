using UnityEngine;

public class EnemyRobotHealth : RobotHealth
{
	public EnemyData data;
	
	protected override void Die(GameObject sender)
	{
		StageManager.instance.CountDefeatedEnemy();
		OnDefeatByPlayer(sender);
		base.Die(sender);
	}

	private void OnDefeatByPlayer(GameObject sender)
	{
		StageManager sm = StageManager.instance;
		
		if(sender.TryGetComponent(out PlayerRobotData prd) && !sm.stateManager.GameIsOver())
		{
			prd.Data.AddDefeatedEnemy(data);
			sm.AddPoints(gameObject, prd.Data, data.score);
			sm.audioManager.PlayEnemyRobotExplosionSound();
		}
	}
}