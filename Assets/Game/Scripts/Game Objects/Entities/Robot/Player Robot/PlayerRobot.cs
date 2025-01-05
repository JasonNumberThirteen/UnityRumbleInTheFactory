using UnityEngine;

public class PlayerRobot : RobotEntity
{
	[SerializeField] private PlayerData playerData;
	
	public override bool IsFriendly() => true;

	public PlayerData GetPlayerData() => playerData;

	private void OnDestroy()
	{
		if(playerData != null && playerData.Spawner != null)
		{
			playerData.Spawner.InitiateRespawn();
		}
	}
}