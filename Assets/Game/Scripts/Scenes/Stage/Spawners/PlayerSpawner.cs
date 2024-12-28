public class PlayerSpawner : EntitySpawner
{
	public PlayerData playerData;
	public Timer spawnTimer, respawnTimer;

	public void InitiateRespawn() => respawnTimer.ResetTimer();

	public void AttemptToRespawn()
	{
		if(playerData.Lives-- > 0)
		{
			spawnTimer.ResetTimer();
		}
		else
		{
			StageManager.instance.playersManager.CheckPlayersLives();
		}
	}

	private void Awake()
	{
		RegisterToListeners(true);
	}

	private void Start() => playerData.Spawner = this;

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			entitySpawnedEvent.AddListener(OnEntitySpawned);
		}
		else
		{
			entitySpawnedEvent.RemoveListener(OnEntitySpawned);
		}
	}

	private void OnEntitySpawned()
	{
		playerData.ResetRank();
	}
}