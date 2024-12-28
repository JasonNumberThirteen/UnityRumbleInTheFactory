using UnityEngine;

public class PlayerRobotHealth : RobotHealth
{
	protected override void Die(GameObject sender)
	{
		if(TryGetComponent(out PlayerRobotData prd))
		{
			prd.Data.Spawner.InitiateRespawn();
		}
		
		base.Die(sender);
	}
}