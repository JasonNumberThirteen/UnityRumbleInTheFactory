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

	protected override StageEventType GetStageEventTypeOnDestructionEvent() => StageEventType.PlayerWasDestroyed;

	public PlayerRobotData GetPlayerRobotData() => playerRobotData;

	protected override void Awake()
	{
		base.Awake();

		playerRobotsDataManager = ObjectMethods.FindComponentOfType<PlayerRobotsDataManager>();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		
		if(playerRobotData != null && playerRobotData.Spawner != null)
		{
			playerRobotData.Spawner.InitiateRespawn();
		}
	}
}