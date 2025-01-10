using UnityEngine;

public class PlayerRobotEntity : RobotEntity
{
	[SerializeField] private PlayerRobotData playerRobotData;

	private PlayersDataManager playersDataManager;
	
	public override bool IsFriendly() => true;

	public override void OnLifeBonusCollected(int lives)
	{
		var playerRobotData = GetPlayerRobotData();

		if(playersDataManager != null && playerRobotData != null)
		{
			playersDataManager.ModifyLives(playerRobotData, lives);
		}
	}

	public PlayerRobotData GetPlayerRobotData() => playerRobotData;

	protected override void Awake()
	{
		base.Awake();

		playersDataManager = FindAnyObjectByType<PlayersDataManager>(FindObjectsInactive.Include);
	}

	private void OnDestroy()
	{
		if(playerRobotData != null && playerRobotData.Spawner != null)
		{
			playerRobotData.Spawner.InitiateRespawn();
		}
	}
}