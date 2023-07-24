public class EnemyRobotHealth : RobotHealth
{
	public EnemyData data;
	public PlayerData playerData;
	
	protected override void Die()
	{
		playerData.Score += data.score;
		++StageManager.instance.DefeatedEnemies;

		playerData.AddDefeatedEnemy(data);
		StageManager.instance.uiManager.CreateGainedPointsCounter(gameObject.transform.position, data.score);
		base.Die();
	}
}