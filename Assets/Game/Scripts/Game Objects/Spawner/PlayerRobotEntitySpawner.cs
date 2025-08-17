using UnityEngine;

public class PlayerRobotEntitySpawner : EntitySpawner
{
	public int NumberOfSpawns {get; private set;}
	
	[SerializeField] private PlayerRobotData playerRobotData;
	[SerializeField, Min(0f)] private float respawnDelay = 1f;

	private PlayerRobotsDataManager playerRobotsDataManager;

	public PlayerRobotData GetPlayerRobotData() => playerRobotData;
	public float GetRespawnDelay() => respawnDelay;

	public void RespawnEntityIfPossible()
	{
		if(playerRobotData == null || playerRobotData.Lives == 0)
		{
			return;
		}

		timer.StartTimer();
		playerRobotData.ResetRank();

		if(playerRobotsDataManager != null)
		{
			playerRobotsDataManager.ModifyLives(playerRobotData, -1);
		}
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

	private void OnEntityWasSpawned(GameObject go)
	{
		++NumberOfSpawns;
	}

	private void Start()
	{
		if(playerRobotData != null)
		{
			playerRobotData.Spawner = this;
		}
	}
}