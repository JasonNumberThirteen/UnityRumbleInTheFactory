public class PlayerRobot : Robot
{
	public override bool IsFriendly() => true;

	private void OnDestroy()
	{
		if(TryGetComponent(out PlayerRobotData prd))
		{
			prd.Data.Spawner.InitiateRespawn();
		}
	}
}