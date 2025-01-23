using UnityEngine;

public class PlayerRobotEntitySpawner : EntitySpawner
{
	[SerializeField] private PlayerRobotData playerRobotData;
	[SerializeField, Min(0f)] private float respawnDelay = 1f;

	private PlayerRobotsDataManager playerRobotsDataManager;

	public void InitiateRespawn()
	{
		Invoke(nameof(AttemptToRespawn), respawnDelay);
	}

	protected override void Awake()
	{
		base.Awake();

		playerRobotsDataManager = ObjectMethods.FindComponentOfType<PlayerRobotsDataManager>();
	}

	private void Start()
	{
		if(playerRobotData != null)
		{
			playerRobotData.Spawner = this;
		}
	}

	private void AttemptToRespawn()
	{
		if(playerRobotData != null && playerRobotData.Lives > 0)
		{
			timer.StartTimer();
			playerRobotData.ResetRank();

			if(playerRobotsDataManager != null)
			{
				playerRobotsDataManager.ModifyLives(playerRobotData, -1);
			}
		}
		else if(playerRobotsDataManager != null)
		{
			playerRobotsDataManager.CheckPlayersLives();
		}
	}
}