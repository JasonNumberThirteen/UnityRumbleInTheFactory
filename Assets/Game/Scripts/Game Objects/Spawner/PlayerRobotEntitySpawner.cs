using UnityEngine;

public class PlayerRobotEntitySpawner : EntitySpawner
{
	[SerializeField] private PlayerRobotData playerRobotData;
	[SerializeField, Min(0f)] private float respawnDelay = 1f;

	private PlayersDataManager playersDataManager;

	public void InitiateRespawn()
	{
		Invoke(nameof(AttemptToRespawn), respawnDelay);
	}

	protected override void Awake()
	{
		base.Awake();

		playersDataManager = FindAnyObjectByType<PlayersDataManager>();
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
			timer.ResetTimer();
			playerRobotData.ResetRank();

			if(playersDataManager != null)
			{
				playersDataManager.ModifyLives(playerRobotData, -1);
			}
		}
		else if(playersDataManager != null)
		{
			playersDataManager.CheckPlayersLives();
		}
	}
}