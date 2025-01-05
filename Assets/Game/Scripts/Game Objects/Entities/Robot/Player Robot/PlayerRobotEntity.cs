using UnityEngine;

public class PlayerRobotEntity : RobotEntity
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