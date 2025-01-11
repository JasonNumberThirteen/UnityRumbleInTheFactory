using UnityEngine;

public class PlayerRobotEntity : RobotEntity
{
	[SerializeField] private PlayerRobotData playerRobotData;

	private PlayerRobotsDataManager playerRobotsDataManager;
	
	public override bool IsFriendly() => true;

	public override void OnLifeBonusCollected(int lives)
	{
		var playerRobotData = GetPlayerRobotData();

		if(playerRobotsDataManager != null && playerRobotData != null)
		{
			playerRobotsDataManager.ModifyLives(playerRobotData, lives);
		}
	}

	public PlayerRobotData GetPlayerRobotData() => playerRobotData;

	protected override void Awake()
	{
		base.Awake();

		playerRobotsDataManager = FindAnyObjectByType<PlayerRobotsDataManager>(FindObjectsInactive.Include);
	}

	private void OnDestroy()
	{
		if(playerRobotData != null && playerRobotData.Spawner != null)
		{
			playerRobotData.Spawner.InitiateRespawn();
		}
	}
}