public class PlayerRobotHealth : RobotHealth
{
	protected override void Explode()
	{
		PlayerRobotRespawn respawn = GetComponent<PlayerRobotRespawn>();
		
		if(respawn != null)
		{
			StageManager.instance.InitiatePlayerRespawn(respawn);
		}

		base.Explode();
	}
}