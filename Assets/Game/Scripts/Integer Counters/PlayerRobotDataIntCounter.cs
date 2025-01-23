using UnityEngine;

public class PlayerRobotDataIntCounter : IntCounter
{
	[SerializeField] private PlayerRobotData playerRobotData;

	public PlayerRobotData GetPlayerRobotData() => playerRobotData;
}