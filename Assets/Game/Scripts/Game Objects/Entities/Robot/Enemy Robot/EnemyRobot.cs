public class EnemyRobot : RobotEntity
{
	public override bool IsFriendly() => false;

	private void OnDestroy()
	{
		StageManager.instance.CountDefeatedEnemy();
	}
}