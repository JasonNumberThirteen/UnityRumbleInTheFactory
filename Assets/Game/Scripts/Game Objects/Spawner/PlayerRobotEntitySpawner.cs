using UnityEngine;

public class PlayerRobotEntitySpawner : EntitySpawner
{
	public int NumberOfSpawns {get; private set;}
	
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

	protected override void RegisterToListeners(bool register)
	{
		base.RegisterToListeners(register);

		if(register)
		{
			entityWasSpawnedEvent.AddListener(OnEntityWasSpawned);
		}
		else
		{
			entityWasSpawnedEvent.RemoveListener(OnEntityWasSpawned);
		}
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
			if(playerRobotData != null)
			{
				playerRobotData.IsAlive = false;
			}
			
			playerRobotsDataManager.CheckPlayersLives();
		}
	}

	private void OnEntityWasSpawned(GameObject go)
	{
		++NumberOfSpawns;
	}
}