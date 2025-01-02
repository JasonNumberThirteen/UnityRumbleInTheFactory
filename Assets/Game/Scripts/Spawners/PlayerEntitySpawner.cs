using UnityEngine;

public class PlayerEntitySpawner : EntitySpawner
{
	[SerializeField] private PlayerData playerData;
	[SerializeField, Min(0f)] private float respawnDelay = 1f;

	public PlayerData GetPlayerData() => playerData;

	public void InitiateRespawn()
	{
		Invoke(nameof(ResetTimer), respawnDelay);
	}

	protected override void RegisterToListeners(bool register)
	{
		base.RegisterToListeners(register);
		
		if(register)
		{
			timer.onEnd.AddListener(AttemptToRespawn);
			entitySpawnedEvent.AddListener(OnEntitySpawned);
		}
		else
		{
			timer.onEnd.RemoveListener(AttemptToRespawn);
			entitySpawnedEvent.RemoveListener(OnEntitySpawned);
		}
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
		if(playerData != null && playerData.Lives-- > 0)
		{
			timer.ResetTimer();
		}
		else
		{
			StageManager.instance.CheckPlayersLives();
		}
	}

	private void OnEntitySpawned()
	{
		if(playerData != null)
		{
			playerData.ResetRank();
		}
	}

	private void ResetTimer()
	{
		timer.ResetTimer();
	}
}