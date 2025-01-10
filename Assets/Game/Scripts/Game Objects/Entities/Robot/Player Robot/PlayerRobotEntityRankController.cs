using UnityEngine;

[RequireComponent(typeof(PlayerRobotEntity))]
public class PlayerRobotEntityRankController : RobotEntityRankController
{
	private PlayerRobotEntity playerRobotEntity;

	private void Awake()
	{
		playerRobotEntity = GetComponent<PlayerRobotEntity>();
	}

	private void Start()
	{
		var playerRobotData = playerRobotEntity.GetPlayerRobotData();

		if(playerRobotData != null)
		{
			rankChangedEvent?.Invoke(playerRobotData.GetRank());
		}
	}
}