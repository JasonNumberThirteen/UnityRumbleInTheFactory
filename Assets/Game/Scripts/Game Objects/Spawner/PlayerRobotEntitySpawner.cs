using UnityEngine;

public class PlayerRobotEntitySpawner : EntitySpawner
{
	[SerializeField] private PlayerData playerData;
	[SerializeField, Min(0f)] private float respawnDelay = 1f;

	private PlayersDataManager playersDataManager;

	public void InitiateRespawn()
	{
		Invoke(nameof(AttemptToRespawn), respawnDelay);
	}

	protected override void RegisterToListeners(bool register)
	{
		base.RegisterToListeners(register);
		
		if(register)
		{
			entitySpawnedEvent.AddListener(OnEntitySpawned);
		}
		else
		{
			entitySpawnedEvent.RemoveListener(OnEntitySpawned);
		}
	}

	protected override void Awake()
	{
		base.Awake();

		playersDataManager = FindAnyObjectByType<PlayersDataManager>();
	}

	private void Start()
	{
		if(playerData != null)
		{
			playerData.Spawner = this;
		}
	}

	private void AttemptToRespawn()
	{
		if(playerData != null && playerData.Lives > 0)
		{
			timer.ResetTimer();

			if(playersDataManager != null)
			{
				playersDataManager.ModifyLives(playerData, -1);
			}
		}
		else if(playersDataManager != null)
		{
			playersDataManager.CheckPlayersLives();
		}
	}

	private void OnEntitySpawned()
	{
		if(playerData != null)
		{
			playerData.ResetRank();
		}
	}
}