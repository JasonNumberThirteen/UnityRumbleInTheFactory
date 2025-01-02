public class EnemyRobot : Robot
{
	public override bool IsFriendly() => false;

	private void OnDestroy()
	{
		StageManager.instance.CountDefeatedEnemy();
	}
}