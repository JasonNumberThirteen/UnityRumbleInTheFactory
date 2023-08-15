using UnityEngine;

public class PlayerRobotHealth : RobotHealth
{
	protected override void Die(GameObject sender)
	{
		StageManager.instance.InitiatePlayerRespawn();
		base.Die(sender);
	}
}