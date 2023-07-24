public class PlayerRobotHealth : RobotHealth
{
	protected override void Die()
	{
		StageManager.instance.InitiatePlayerRespawn();
		base.Die();
	}
}