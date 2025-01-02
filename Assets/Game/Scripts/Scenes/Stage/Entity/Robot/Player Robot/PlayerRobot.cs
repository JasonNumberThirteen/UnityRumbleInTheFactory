using UnityEngine;

public class PlayerRobot : Robot
{
	[SerializeField] private PlayerData playerData;
	
	public override bool IsFriendly() => true;

	public PlayerData GetPlayerData() => playerData;

	private void OnDestroy()
	{
		if(playerData != null)
		{
			playerData.Spawner.InitiateRespawn();
		}
	}
}