using UnityEngine;

public class PlayerRobotEntity : RobotEntity
{
	[SerializeField] private PlayerData playerData;

	private PlayersDataManager playersDataManager;
	
	public override bool IsFriendly() => true;

	public override void OnLifeBonusCollected(int lives)
	{
		var playerData = GetPlayerData();

		if(playersDataManager != null && playerData != null)
		{
			playersDataManager.ModifyLives(playerData, lives);
		}
	}

	public PlayerData GetPlayerData() => playerData;

	protected override void Awake()
	{
		base.Awake();

		playersDataManager = FindAnyObjectByType<PlayersDataManager>(FindObjectsInactive.Include);
	}

	private void OnDestroy()
	{
		if(playerData != null && playerData.Spawner != null)
		{
			playerData.Spawner.InitiateRespawn();
		}
	}
}