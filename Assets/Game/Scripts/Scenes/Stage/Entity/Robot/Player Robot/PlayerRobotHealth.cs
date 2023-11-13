using UnityEngine;

public class PlayerRobotHealth : RobotHealth
{
	protected override void Die(GameObject sender)
	{
		if(TryGetComponent(out PlayerRobotData prd))
		{
			prd.Data.spawner.InitiateRespawn();
		}
		
		base.Die(sender);
	}
}