public class PlayerRobotHealth : RobotHealth
{
	protected override void Explode()
	{
		StageManager.instance.InitiatePlayerRespawn();
		base.Explode();
	}
}