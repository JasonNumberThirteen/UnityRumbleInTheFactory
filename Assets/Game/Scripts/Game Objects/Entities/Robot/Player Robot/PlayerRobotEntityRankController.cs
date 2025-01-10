using UnityEngine;

[RequireComponent(typeof(PlayerRobotEntity))]
public class PlayerRobotEntityRankController : RobotEntityRankController
{
	protected override void Start()
	{
		if(robotData != null)
		{
			rankChangedEvent?.Invoke(robotData.GetRank());
		}
	}
}