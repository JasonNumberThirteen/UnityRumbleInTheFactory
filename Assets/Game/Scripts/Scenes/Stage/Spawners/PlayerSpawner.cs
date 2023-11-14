public class PlayerSpawner : EntitySpawner
{
	public PlayerData playerData;
	public Timer spawnTimer, respawnTimer;

	public void InitiateRespawn() => respawnTimer.ResetTimer();

	public void AttemptToRespawn()
	{
		if(playerData.Lives-- > 0)
		{
			playerData.OnRespawn();
			spawnTimer.ResetTimer();
		}
		else
		{
			playerData.lostAllLives = true;
			
			StageManager.instance.playersManager.CheckPlayersLives();
		}
	}

	private void Start() => playerData.spawner = this;
}